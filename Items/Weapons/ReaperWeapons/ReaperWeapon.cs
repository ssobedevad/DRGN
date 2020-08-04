using DRGN.Projectiles.Reaper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.ReaperWeapons
{
    // This class handles everything for our custom damage class
    // Any class that we wish to be using our custom damage class will derive from this class, instead of ModItem
    public abstract class ReaperWeapon : ModItem
    {
        public override bool CloneNewInstances => true;
        public int BloodHuntRange = 0;
        private int BloodHuntRangeReal = 0;
        public float DashSpeed = 1;
        public int scytheThrowStyle;
        public Texture2D scytheThrownTexture;
        public int type;

        public const short Normal = 0;
        public const short SeperateSprite = 1;
        public const short Dagger = 0;
        public const short Scythe = 1;



        private Vector2 startingPos;
        private int mode;


        public override void HoldItem(Player player)
        {
            if (BloodHuntRange != 0)
            {
                BloodHuntRangeReal = BloodHuntRange + player.GetModPlayer<ReaperPlayer>().bloodHuntExtraRange;
            }
            if (player.GetModPlayer<ReaperPlayer>().isReaper && BloodHuntRangeReal > 0 && player.GetModPlayer<ReaperPlayer>().HuntedTarget != -1 && player.GetModPlayer<ReaperPlayer>().stabDashCd == 0)
            {
                float dist = Vector2.Distance(player.Center, Main.npc[player.GetModPlayer<ReaperPlayer>().HuntedTarget].Center);
                int Alpha = dist < BloodHuntRangeReal ? 0 : dist < BloodHuntRangeReal * 2 ? 120 : 220;
                for (int i = 0; i < 50; i++)
                {
                    int dustid =
                        Dust.NewDust(
                        new Vector2(player.Center.X + (float)Math.Cos((Math.PI / 25f) * i) * BloodHuntRangeReal,
                        player.Center.Y + (float)Math.Sin((Math.PI / 25f) * i) * BloodHuntRangeReal),
                                              1,
                                              1,
                                              DustID.Blood,
                                              0,
                                              0,
                                              Alpha
                                              );
                    Main.dust[dustid].noGravity = true;




                }
            }
        }

        // Custom items should override this to set their defaults
        public virtual void SafeSetDefaults()
        {

        }

        // By making the override sealed, we prevent derived classes from further overriding the method and enforcing the use of SafeSetDefaults()
        // We do this to ensure that the vanilla damage types are always set to false, which makes the custom damage type work
        public sealed override void SetDefaults()
        {
            SafeSetDefaults();
            // all vanilla damage types must be false for custom damage types to work
            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.thrown = false;
            item.summon = false;
        }

        // As a modder, you could also opt to make these overrides also sealed. Up to the modder
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= ReaperPlayer.ModPlayer(player).reaperDamageMult;
            mult += ReaperPlayer.ModPlayer(player).numSouls * ReaperPlayer.ModPlayer(player).damageIncPerSoul;

        }

        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            // Adds knockback bonuses
            knockback += ReaperPlayer.ModPlayer(player).reaperKnockback;
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            // Adds crit bonuses

            crit += ReaperPlayer.ModPlayer(player).reaperCrit;

        }

        // Because we want the damage tooltip to show our custom damage, we need to modify it
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Get the vanilla damage tooltip
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
            if (tt != null)
            {
                // We want to grab the last word of the tooltip, which is the translated word for 'damage' (depending on what language the player is using)
                // So we split the string by whitespace, and grab the last word from the returned arrays to get the damage word, and the first to get the damage shown in the tooltip
                string[] splitText = tt.text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                // Change the tooltip text
                tt.text = damageValue + " reaper " + damageWord;
            }


        }
        public override bool CanUseItem(Player player)
        {


            if (player.altFunctionUse == 2 && type == Dagger)
            {

                int tpNPC = player.GetModPlayer<ReaperPlayer>().HuntedTarget;

                if (tpNPC != -1 && player.GetModPlayer<ReaperPlayer>().stabDashCd == 0)
                {
                    if (Vector2.Distance(Main.MouseWorld, Main.npc[tpNPC].Center) > 80 || Vector2.Distance(player.Center, Main.npc[tpNPC].Center) > BloodHuntRangeReal)
                    {


                        tpNPC = -1;
                    }

                    if (tpNPC != -1)
                    {
                        mode = 0;
                        
                        return true;

                    }

                }
                return false;
            }
            if (player.altFunctionUse == 2 && type == Scythe)
            {



                if (player.GetModPlayer<ReaperPlayer>().scytheThrowCd == 0)
                {


                    mode = 0;
                    item.noMelee = true;
                    item.noUseGraphic = true;
                    return true;



                }
                return false;
            }
            item.noMelee = false;
            item.noUseGraphic = false;
            mode = 0;
            return true;

        }
        public override void UseStyle(Player player)
        {

            if (player.altFunctionUse == 2 && player.GetModPlayer<ReaperPlayer>().HuntedTarget != -1 && BloodHuntRangeReal > 0 && type == Dagger)
            {
                player.immune = true;
                player.immuneTime = 90;
                NPC tpNPC = Main.npc[player.GetModPlayer<ReaperPlayer>().HuntedTarget];
                int Side = player.Center.X > tpNPC.Center.X ? 1 : -1;
                player.ChangeDir(Side * -1);
                Main.NewText(player.itemAnimation);
                Main.NewText(player.itemAnimationMax);
                if (player.itemAnimation > player.itemAnimationMax * 0.5f)
                {
                    if (mode == 0) { startingPos = player.Center; mode = 1; }

                    Vector2 targetPos = tpNPC.Center + new Vector2(Side * tpNPC.width * 2, -100);
                    targetPos -= startingPos;
                    player.Center = startingPos + (targetPos * ((float)(player.itemAnimationMax - player.itemAnimation) / (player.itemAnimationMax * 0.5f)));



                }

                else if (player.itemAnimation > player.itemAnimationMax * 0.1f)
                {
                    if (mode == 1) { startingPos = player.Center; mode = 2; }

                    Vector2 targetPos = tpNPC.Center + new Vector2(Side, -10);
                    targetPos -= startingPos;
                    player.Center = startingPos + (targetPos * ((float)(player.itemAnimationMax * 0.5f - player.itemAnimation) / (player.itemAnimationMax * 0.4f)));

                }
                else if (mode == 2 && Main.netMode != NetmodeID.MultiplayerClient)
                { tpNPC.StrikeNPC(getStabDamage(player,tpNPC), item.knockBack * 2.5f * player.GetModPlayer<ReaperPlayer>().reaperKnockback, Side, true); NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, tpNPC.whoAmI); mode = 3; }

                else
                {


                    player.GetModPlayer<ReaperPlayer>().stabDashCd = item.useTime * 20;
                    player.velocity.X = Side * DashSpeed; player.velocity.Y = -DashSpeed/2;
                    
                    mode = 0;
                }
            }
            else if (type == Scythe)
            {
                player.ChangeDir(Main.MouseWorld.X > player.Center.X ? 1 : -1);
                if (player.altFunctionUse == 2)
                {
                    if (player.itemAnimation > player.itemAnimationMax * 0.6f)
                    {
                        if (mode == 0)
                        {
                            player.velocity = new Vector2(-player.direction * DashSpeed * 1.5f, -5);
                            mode = 1;
                        }

                       





                    }
                    else if (player.itemAnimation > player.itemAnimationMax * 0.2f)
                    {
                        player.velocity.X *= 0.95f;
                       



                    }
                    else
                    {
                        if (mode == 1)
                        {
                            Vector2 vel = Vector2.Normalize(Main.MouseWorld - player.Center) * DashSpeed * 2f;
                            int projid = Projectile.NewProjectile(player.Center, vel, mod.ProjectileType("ReaperScythe"), (int)(item.damage * 2f * player.GetModPlayer<ReaperPlayer>().reaperDamageMult * player.allDamageMult * player.GetModPlayer<ReaperPlayer>().numSouls * player.GetModPlayer<ReaperPlayer>().damageIncPerSoul), item.knockBack, player.whoAmI,30 , item.crit + player.GetModPlayer<ReaperPlayer>().reaperCrit);
                            ReaperScythe Rs = Main.projectile[projid].modProjectile as ReaperScythe;
                            if (scytheThrowStyle == Normal)
                            {
                                Rs.projectileTexture = ModContent.GetTexture(Texture);
                            }
                            else if (scytheThrowStyle == SeperateSprite)
                            {
                                Rs.projectileTexture = scytheThrownTexture;
                            }
                            mode = 0;
                        }
                        player.GetModPlayer<ReaperPlayer>().scytheThrowCd = item.useTime * 4; player.velocity.X *= 0.75f; }






                }
                else if (mode == 0 && player.itemAnimation < player.itemAnimationMax * 0.4f && player.velocity.X < DashSpeed * 2) { player.velocity += Vector2.Normalize(Main.MouseWorld - player.Center) * DashSpeed; player.fallStart = (int)(player.Center.Y/16f); mode = 1; }
                else if(player.itemAnimation < player.itemAnimationMax * 0.1f) { player.velocity.X *= 0.8f; }
            }
        }
        public int getStabDamage(Player player , NPC target)
        {
            int baseDamage = (int)(item.damage * 2.5f);
            float damageMult = player.GetModPlayer<ReaperPlayer>().reaperDamageMult * player.GetModPlayer<ReaperPlayer>().reaperCritDamageMult * player.allDamageMult * player.GetModPlayer<ReaperPlayer>().numSouls * player.GetModPlayer<ReaperPlayer>().damageIncPerSoul;
            baseDamage = (int)(baseDamage * damageMult);
            int armorPenetration = player.armorPenetration + player.GetModPlayer<ReaperPlayer>().reaperCritArmorPen;
            if (player.GetModPlayer<DRGNPlayer>().wofEquip) { baseDamage = (int)(baseDamage * 1.25); armorPenetration += 10; }
            if (player.GetModPlayer<DRGNPlayer>().tvEquip) { baseDamage = (int)(baseDamage * 1.1); }
            if (player.GetModPlayer<DRGNPlayer>().ksEquip) { armorPenetration += 8; }
            baseDamage += armorPenetration;
            if(armorPenetration > target.defense) { baseDamage = (int)(baseDamage * (1 + (armorPenetration - target.defense) * 0.025)); }
            return baseDamage;

        }

        // Make sure you can't use the item if you don't have enough resource and then use 10 resource otherwise.

        public override bool AltFunctionUse(Player player)
        {
            return player.GetModPlayer<ReaperPlayer>().isReaper;
        }

        public bool SolidTiles(int startX, int endX, int startY, int endY)
        {
            if (startX < 0)
            {
                return true;
            }
            if (endX >= Main.maxTilesX)
            {
                return true;
            }
            if (startY < 0)
            {
                return true;
            }
            if (endY >= Main.maxTilesY)
            {
                return true;
            }
            for (int vector = startX; vector < endX + 1; vector++)
            {
                for (int i = startY; i < endY + 1; i++)
                {

                    if (Main.tile[vector, i].active() && !Main.tile[vector, i].inActive() && Main.tileSolid[Main.tile[vector, i].type] && Main.tile[vector, i].type != TileID.Platforms)
                    {
                        return true;
                    }
                }
            }
            return false;
        }




    }
}
