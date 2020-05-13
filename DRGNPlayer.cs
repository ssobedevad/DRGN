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
        public static bool VoidBiome = false;
        public static bool heartEmblem;
        public static bool secondlife;
        public static int canDodge;
        public static int lifeCounter = 0;
        public static bool NinjaSuit;
        public static bool playerNameCheck;
        public List<string> Boosters;
        public static int[] VoidEffect = new int[255];
        public override void ResetEffects()
        {
            NinjaSuit = false;
            secondlife = false;
            //if (Boosters.Contains(player.name)) { playerNameCheck = true; }
            //if (heartEmblem == true && playerNameCheck == true) { } //player.statLifeMax2 += 50; }
            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                Item item = player.armor[i];

                //Set the flag for the ExampleDashAccessory being equipped if we have it equipped OR immediately return if any of the accessories are
                // one of the higher-priority ones
                if (item.type == mod.ItemType("NinjaSuit"))
                {
                    NinjaSuit = true;
                    canDodge += 1;
                    if (canDodge >= 1200) { player.AddBuff(mod.BuffType("FromTheShadows"),2); }
                }
                else if (item.type == mod.ItemType("EssenceofExpert"))
                { secondlife = true; if (lifeCounter > 0) { lifeCounter -= 1; } }else {  player.AddBuff(mod.BuffType("Revival"),2);  }
               


            }
            player.minionDamage = player.magicDamage;
        }
        public override void Load(TagCompound tag)
        {
            
            IList<string> IlifeBoosters = tag.GetList<string>("lifeBoosters");
            List<string> lifeBoosters = new List<string>(IlifeBoosters.Select(X => (string)X));
            
           
            heartEmblem = IlifeBoosters.Contains("heartEmblem");
            NinjaSuit = false;
            Boosters = lifeBoosters;
        }
        
        public override TagCompound Save()
        {
            
            var lifeBoosters = new List<string>();
            lifeBoosters.Add(player.name);
            if (heartEmblem) { lifeBoosters.Add("heartEmblem"); }
            return new TagCompound
            {
               
                ["lifeBoosters"] = lifeBoosters
            };

        }

        public override void UpdateBiomes()
        {
            DragonBiome = (DRGNModWorld.DragonDen > 20);
            VoidBiome = (DRGNModWorld.isVoidBiome > 20);
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (NinjaSuit == true && canDodge >= 1200)
            {

                
                player.statLife += damage;
                player.HealEffect(damage);
                player.immune = true;
                player.immuneTime = 100;
                canDodge = 0;
                damage = 0;
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
          
            if (secondlife == true && lifeCounter == 0)
            {

                player.statLife = player.statLifeMax2;
                player.HealEffect(player.statLifeMax2);
                player.immune = true;
                player.immuneTime = 100;
                lifeCounter = 12000;
                return false; }
            else { return true; }
        
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

        }






    }
}
