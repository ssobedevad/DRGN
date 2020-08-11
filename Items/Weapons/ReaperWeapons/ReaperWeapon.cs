using DRGN.Buffs;
using DRGN.Projectiles.Reaper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

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
        public Texture2D projectileText;
        public Texture2D chaintext;
        public int type;

        public const short Normal = 0;
        public const short SeperateSprite = 1;
        public const short Dagger = 0;
        public const short Scythe = 1;
        public const short Hook = 2;


       
        private int mode;
        

        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            if (pre == -3) { return true; }
            return true;
        }
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return Main.rand.Next(DRGN.meleePrefixes);
        }
        public override bool AllowPrefix(int pre)
        {


            if (DRGN.meleePrefixes.Contains(pre)) { return true; }
            return false;

        }
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
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (item.shoot > ProjectileID.None)
            {
                Projectile proj = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), mod.ProjectileType("ReaperChain"), damage, knockBack, player.whoAmI, DashSpeed * 12, item.crit + player.GetModPlayer<ReaperPlayer>().reaperCrit);
                ReaperChain Rc = proj.modProjectile as ReaperChain;
                Rc.projectileTexture = projectileText;
                Rc.chainTexture = chaintext;
                Rc.ownerItem = item.modItem;
            }


            return false;
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
            item.autoReuse = true;
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
        public void RetractAllHooks(Player player)
        {
            foreach (HookedData datavalue in player.GetModPlayer<ReaperPlayer>().hookedTargets.Values.ToList())
            {
                NPC target = Main.npc[datavalue.npc];
                int damage = 0;
                bool crit = false;
                
                for (int i = 0; i < datavalue.ownerProjs.Count; i++)
                {
                    bool Crit = datavalue.crit.Contains(datavalue.ownerProjs[i].projectile.whoAmI);
                    damage += (int)(datavalue.ownerProjs[i].baseDamage * (Crit? 1.5f : 0.75f)); ;
                    damage += getDamageIncFromArmorPen(player, Crit, target);
                    datavalue.ownerProjs[i].stuckToNPC = -2;
                }
                
                if (target.boss)
                {
                    for (int i = 0; i < datavalue.stack * 0.5f; i++)
                    {
                        Projectile.NewProjectile(target.Center, new Vector2(Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8)), mod.ProjectileType("ReaperSoulProj"), ReaperPlayer.getSoulDamage(), 0, player.whoAmI);
                    }
                }
                else { target.GetGlobalNPC<ReaperGlobalNPC>().soulReward += (int)(datavalue.stack * 0.5f); }
                if (player.GetModPlayer<ReaperPlayer>().HuntedTarget == datavalue.npc) { damage *= 2; crit = true; }
                if (datavalue.crit.Count > 0) { crit = true; }
                target.StrikeNPC(damage, 0f, 1, crit);
                int healing = (int)(DashSpeed * (DRGNModWorld.MentalMode ? 2f : Main.expertMode ? 1.5f : 1f)) + (int)(damage * (DRGNModWorld.MentalMode ? 0.075f : Main.expertMode ? 0.05f : 0.025f));
                if (player.statLifeMax2 > player.statLife + healing)
                {
                    player.HealEffect(healing);
                    player.statLife += healing;


                }
                else if (player.statLife != player.statLifeMax2)
                {
                    player.HealEffect(player.statLifeMax2 - player.statLife);
                    player.statLife = player.statLifeMax2;
                }
                
                player.GetModPlayer<ReaperPlayer>().hookedTargets.Remove(datavalue.npc);


            }
            player.GetModPlayer<ReaperPlayer>().numSouls -= 5;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && type == Hook)
            {

                if (player.GetModPlayer<ReaperPlayer>().hookedTargets.Count > 0 && player.GetModPlayer<ReaperPlayer>().numSouls >= 5)
                {

                    item.shoot = ProjectileID.None;
                   
                    item.noUseGraphic = true;
                    return true;


                }
                return false;

            }
            if (type == Hook && player.altFunctionUse != 2)
            {
                item.shoot = ProjectileID.WoodenArrowFriendly;
                
                item.noUseGraphic = true;
                return true;
            }

            if (player.altFunctionUse == 2 && type == Dagger)
            {

                int tpNPC = player.GetModPlayer<ReaperPlayer>().HuntedTarget;

                if (tpNPC != -1 && player.GetModPlayer<ReaperPlayer>().stabDashCd == 0)
                {
                    if (Vector2.Distance(Main.MouseWorld, Main.npc[tpNPC].Center) > Main.npc[tpNPC].width * 5f || Vector2.Distance(player.Center, Main.npc[tpNPC].Center) > BloodHuntRangeReal)
                    {


                        tpNPC = -1;
                    }

                    if (tpNPC != -1 && player.GetModPlayer<ReaperPlayer>().numSouls >= 5)
                    {
                        
                        mode = 0;
                        item.noMelee = true;
                        return true;

                    }

                }
                return false;
            }
            if (player.altFunctionUse == 2 && type == Scythe)
            {



                if (player.GetModPlayer<ReaperPlayer>().scytheThrowCd == 0 && player.GetModPlayer<ReaperPlayer>().numSouls >= 5)
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
                if (mode == 0)
                {
                    Vector2 vel = Vector2.Normalize(Main.MouseWorld - player.Center) * DashSpeed * 2f;
                    int projid = Projectile.NewProjectile(player.Center, vel, mod.ProjectileType("ReaperKnife"), getProjectileDamage(1.5f, player), item.knockBack, player.whoAmI, player.GetModPlayer<ReaperPlayer>().HuntedTarget);
                    ReaperKnife Rk = Main.projectile[projid].modProjectile as ReaperKnife;


                    Rk.projectileTexture = ModContent.GetTexture(Texture);
                    Rk.critChance = item.crit + player.GetModPlayer<ReaperPlayer>().reaperCrit;

                    Rk.ownerItem = item.modItem;
                    player.GetModPlayer<ReaperPlayer>().numSouls -= 5;
                    player.GetModPlayer<ReaperPlayer>().stabDashCd = item.useTime * 10;
                    mode = 1;
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
                            int projid = Projectile.NewProjectile(player.Center, vel, mod.ProjectileType("ReaperScythe"), getProjectileDamage(1.25f, player), item.knockBack, player.whoAmI, DashSpeed * 7, item.crit + player.GetModPlayer<ReaperPlayer>().reaperCrit);
                            ReaperScythe Rs = Main.projectile[projid].modProjectile as ReaperScythe;

                            if (scytheThrowStyle == Normal)
                            {
                                Rs.projectileTexture = ModContent.GetTexture(Texture);
                            }
                            else if (scytheThrowStyle == SeperateSprite)
                            {
                                Rs.projectileTexture = projectileText;
                            }
                            Rs.ownerItem = item.modItem;
                            player.GetModPlayer<ReaperPlayer>().numSouls -= 5;
                            player.GetModPlayer<ReaperPlayer>().scytheThrowCd = item.useTime * 10;
                            mode = 0;
                        }

                        player.velocity.X *= 0.75f;
                    }

                }

            }
        }
        public override bool UseItem(Player player)
        {
           
            if (player.altFunctionUse == 2 && type == Hook)
            {

                if (player.GetModPlayer<ReaperPlayer>().hookedTargets.Count > 0 && player.GetModPlayer<ReaperPlayer>().numSouls >= 5)
                {
                    RetractAllHooks(player);                                   
                }
            }
            return true;
        }

        public int getDamageIncFromArmorPen(Player player, bool crit = false, NPC npc = null)
        {
            int armorPenetration = player.armorPenetration;
            if (crit)
            {
                armorPenetration += player.GetModPlayer<DRGNPlayer>().criticalArmorPen + player.GetModPlayer<ReaperPlayer>().reaperCritArmorPen;
            }
            if(npc != null && armorPenetration > npc.defense) { armorPenetration += npc.defense - armorPenetration; }
            

            return armorPenetration;

        }
        public int getProjectileDamage(float mult, Player player)
        {
            int baseDamage = (int)(item.damage * mult);
            float damageMult = player.GetModPlayer<ReaperPlayer>().reaperDamageMult * player.GetModPlayer<ReaperPlayer>().reaperCritDamageMult * player.allDamageMult * (1 + player.GetModPlayer<ReaperPlayer>().numSouls * player.GetModPlayer<ReaperPlayer>().damageIncPerSoul);
            baseDamage = (int)(baseDamage * damageMult);
           
            
            
            baseDamage += getDamageIncFromArmorPen(player, true); 
            baseDamage = (int)(baseDamage * player.GetModPlayer<DRGNPlayer>().criticalDamageMult);
            baseDamage = (int)(baseDamage * (1f + player.ownedProjectileCounts[mod.ProjectileType("ReaperScythe")] * 0.1f));
            return baseDamage;

        }
        

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (player.altFunctionUse != 2) { target.AddBuff(mod.BuffType("MarkedForDeath"), type == Dagger ? 240 : 120); }
            if (target.boss && crit) { Projectile.NewProjectile(target.Center, new Vector2(Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8)), mod.ProjectileType("ReaperSoulProj"), ReaperPlayer.getSoulDamage(), 0, player.whoAmI); }
        }
        // Make sure you can't use the item if you don't have enough resource and then use 10 resource otherwise.

        public override bool AltFunctionUse(Player player)
        {
            return player.GetModPlayer<ReaperPlayer>().isReaper;
        }

        




    }
}
