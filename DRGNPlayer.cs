using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System;
using Microsoft.Xna.Framework;
using System.Collections;
using System.Linq;
using Terraria.DataStructures;
using Terraria.Localization;
namespace DRGN
{
    public class DRGNPlayer : ModPlayer
    {
        public static bool DragonBiome = false;
        public static bool AntBiome = false;
        public static bool VoidBiome = false;
        
        public static bool secondlife;
        public static int lifeQuality;
        public static bool NinjaSuit;
        public static bool brawlerGlove;
        public static bool beeVeil;
        public static bool protectorsVeil;
        public static int dodgeCounter;
        public static int dodgeCounterMax;
        public static int lifeCounter;
        public static int lifeCounterMax;
        
        
        public static bool melting;
        public static bool burning;
        public static bool shocked;
        public static bool galacticCurse;
        
        public static int[] VoidEffect = new int[255];

        public int heartEmblem;
        public const int heartEmblemMax = 10;

        public bool lunarBlessing;

        public override void ResetEffects()
        {
            
            NinjaSuit = false;
            secondlife = false;
            brawlerGlove = false;
            beeVeil = false;
            protectorsVeil = false;
            
            if (lunarBlessing) { player.extraAccessorySlots += 1; }
            player.statLifeMax2 += 5 * heartEmblem ;
            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                Item item = player.armor[i];

                
                if (item.type == mod.ItemType("NinjaSuit"))
                {
                    NinjaSuit = true;
                    dodgeCounterMax = 1200;
                    if (dodgeCounter < dodgeCounterMax)
                    {
                        dodgeCounter += 1;
                       
                    }
                    if (dodgeCounter == dodgeCounterMax) { player.AddBuff(mod.BuffType("FromTheShadows"), 2); }
                }
                else if (item.type == mod.ItemType("EssenceofExpert")|| item.type == mod.ItemType("CrystalofCharisma")|| item.type == mod.ItemType("PowderofCourage"))
                {
                    secondlife = true; lifeCounterMax = 12000;
                    if (item.type == mod.ItemType("EssenceofExpert"))
                    {
                        lifeQuality = 10;
                    }
                    else if (item.type == mod.ItemType("CrystalofCharisma"))
                    { lifeQuality = 5; }
                    else if (item.type == mod.ItemType("PowderofCourage"))
                    { lifeQuality = 2; }

                    if (lifeCounter < lifeCounterMax) { lifeCounter += 1; }
                    else { player.AddBuff(mod.BuffType("Revival"), 2); }
                }
                else if (item.type == mod.ItemType("GalactiteBrawlerGloves"))
                { brawlerGlove = true; }
                else if (item.type == mod.ItemType("ProtectorsVeil"))
                { protectorsVeil = true; }
                else if (item.type == mod.ItemType("BeeVeil"))
                { beeVeil = true; }
               
            }
            if (player.FindBuffIndex(mod.BuffType("Melting")) == -1)
            { melting = false; }
            if (player.FindBuffIndex(mod.BuffType("Burning")) == -1)
            { burning = false; }
            if (player.FindBuffIndex(mod.BuffType("GalacticCurse")) == -1)
            { galacticCurse = false; }
            if (player.FindBuffIndex(mod.BuffType("Shocked")) == -1)
            { shocked = false; }

            player.minionDamage = player.magicDamage;
            
        }
        
        
        public override TagCompound Save()
        {

            return new TagCompound
            {

                {"HEmblem", heartEmblem },
                { "LBlessing", lunarBlessing },
                
            };

        }
        public override void Load(TagCompound tag)
        {

            heartEmblem = tag.GetInt("HEmblem");
            lunarBlessing = tag.GetBool("LBlessing");
        }
       
        public override void UpdateBadLifeRegen()
        {
            if (melting)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                player.lifeRegen -= 12;
            }
            if (burning)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                player.lifeRegen -= 24;
            }
            if (galacticCurse)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                player.lifeRegen -= 100;
            }
            if (shocked)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                player.lifeRegen -= 18;
            }

        }

        public override void UpdateBiomes()
        {
            DragonBiome = (DRGNModWorld.DragonDen > 20);
            VoidBiome = (DRGNModWorld.isVoidBiome > 20);
            AntBiome = (DRGNModWorld.isAntBiome > 20);
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (NinjaSuit == true && dodgeCounter == dodgeCounterMax)
            {

                
                player.statLife += damage;
                player.HealEffect(damage);
                player.immune = true;
                player.immuneTime = 100;
                dodgeCounter = 0;
               
                for (int i = 0; i < 55; i++)
                {
                    int DustID = Dust.NewDust(player.Center, 0, 0, 182, 0.0f, 0.0f, 10, default(Color), 2.5f);
                    Main.dust[DustID].noGravity = true;
                }
                return false;

            }
            else
            {
                return true;
            }


        }



              public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
          
            if (secondlife == true && lifeCounter == lifeCounterMax)
            {

                player.statLife = ((int)player.statLifeMax2/10) * lifeQuality;
                player.HealEffect((int)player.statLifeMax2 / 10 * lifeQuality);
                player.immune = true;
                player.immuneTime = 200;
                lifeCounter = 0;
                return false; }
            else { return true; }
        
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (brawlerGlove) { target.AddBuff(mod.BuffType("GalacticCurse"), 280); }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (brawlerGlove) { target.AddBuff(mod.BuffType("GalacticCurse"), 280); }
        }
        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (protectorsVeil)
            {
                
                    Projectile.NewProjectile(player.Center.X + Main.rand.Next(-5, 5), player.Center.Y + Main.rand.Next(-5, 5), 0, 0, mod.ProjectileType("OmegaStarBee"), 280, 1f, player.whoAmI, 0);
                    player.immune = true;
                    player.immuneTime = 140;
                
            }
            else if (beeVeil)
        { for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(player.Center.X + Main.rand.Next(-5,5), player.Center.Y + Main.rand.Next(-5, 5), 0, 0, mod.ProjectileType("StarBee"), 80, 1f,player.whoAmI,0);
                    player.immune = true;
                    player.immuneTime = 75;
                }
         }
        }

        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            Item item = new Item();
            item.SetDefaults(mod.ItemType("MossyBowWood"));
            item.stack = 1;
            items.Add(item);
            Item item2 = new Item();
            item2.SetDefaults(mod.ItemType("SnappedHandle"));
            item2.stack = 1;
            items.Add(item2);
            Item item3 = new Item();
            item3.SetDefaults(mod.ItemType("TornBook"));
            item3.stack = 1;
            items.Add(item3);
            if(Main.expertMode)
            {
                Item item4 = new Item();
                item4.SetDefaults(mod.ItemType("PowderofCourage"));
                item4.stack = 1;
                items.Add(item4);
            }

        }






    }
}
