using Microsoft.Xna.Framework;
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
        public BloodHunt BloodHunt = new BloodHunt(-1, -1);
        public Vector2 startingPos;
        public int mode;

        public override void HoldItem(Player player)
        {

            if (player.GetModPlayer<ReaperPlayer>().isReaper && BloodHunt.style == 1 && BloodHunt.range > 0 && player.GetModPlayer<ReaperPlayer>().HuntedTarget != -1 && player.GetModPlayer<ReaperPlayer>().stabDashCd == 0)
            {
                float dist = Vector2.Distance(player.Center, Main.npc[player.GetModPlayer<ReaperPlayer>().HuntedTarget].Center);
                int Alpha = dist < BloodHunt.range ? 0 : dist < BloodHunt.range* 2 ? 120 : 220;
                for (int i = 0; i < 50; i++)
                {
                    int dustid =
                        Dust.NewDust(
                        new Vector2(player.Center.X + (float)Math.Cos((Math.PI / 25f) * i) * BloodHunt.range,
                        player.Center.Y + (float)Math.Sin((Math.PI / 25f) * i) * BloodHunt.range),
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
            
            item.useTime = 20;
            item.useAnimation = 20;
            if (player.altFunctionUse == 2)
            {
                item.useTime = 60;
                item.useAnimation = 60;
                int tpNPC = player.GetModPlayer<ReaperPlayer>().HuntedTarget;

                if (tpNPC != -1 && player.GetModPlayer<ReaperPlayer>().stabDashCd == 0)
                {
                    if (Vector2.Distance(Main.MouseWorld, Main.npc[tpNPC].Center) > 80 || Vector2.Distance(player.Center, Main.npc[tpNPC].Center) > BloodHunt.range)
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
            return true;

        }
        public override void UseStyle(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                NPC tpNPC= Main.npc[player.GetModPlayer<ReaperPlayer>().HuntedTarget];
                if (player.itemAnimation < player.itemAnimationMax *0.6f)
                {
                    if(mode == 0) { startingPos = player.Center; mode = 1; }
                    int Side = player.Center.X > tpNPC.Center.X ? 1 : -1;
                    Vector2 targetPos = tpNPC.Center + new Vector2(Side * tpNPC.width * 2,-100);
                    targetPos -= startingPos;
                    player.Center = startingPos + (targetPos * (player.itemAnimation/ player.itemAnimationMax*0.6f));



                }
                
                else if (player.itemAnimation < player.itemAnimationMax * 0.85f) 
                {
                    if (mode == 1) { startingPos = player.Center; mode = 2; }
                    int Side = player.Center.X > tpNPC.Center.X ? 1 : -1;
                    Vector2 targetPos = tpNPC.Center + new Vector2(Side * tpNPC.width, -10);
                    targetPos -= startingPos;
                    player.Center = startingPos + (targetPos * (player.itemAnimation - player.itemAnimationMax * 0.6f / player.itemAnimationMax * 0.25f));
                }
                
                else 
                {
                    
                    int Side = player.Center.X > tpNPC.Center.X ? 1 : -1;
                    
                    player.velocity.X = Side * 6; player.velocity.Y = -5;
                    mode = 0;
                }
            }
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
