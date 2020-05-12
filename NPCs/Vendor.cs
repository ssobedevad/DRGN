using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Modules;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.NPCs
{
    [AutoloadHead]
    public class Vendor : ModNPC
    {
        public bool otherShop;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vendor");


        }
        public override void SetDefaults()
        {
            npc.townNPC = true; //This defines if the npc is a town Npc or not
            npc.friendly = true;  //this defines if the npc can hur you or not()
            npc.width = 26; //the npc sprite width
            npc.height = 46;  //the npc sprite height
            npc.aiStyle = 7; //this is the npc ai style, 7 is Pasive Ai
            npc.defense = 25;  //the npc defense
            npc.lifeMax = 250;// the npc life
            npc.HitSound = SoundID.NPCHit1;  //the npc sound when is hit
            npc.DeathSound = SoundID.NPCDeath1;  //the npc sound when he dies
            npc.knockBackResist = 0.5f;  //the npc knockback resistance
            Main.npcFrameCount[npc.type] = 26; //this defines how many frames the npc sprite sheet has
            NPCID.Sets.ExtraFramesCount[npc.type] = 0;
            NPCID.Sets.AttackFrameCount[npc.type] = 0;
            NPCID.Sets.DangerDetectRange[npc.type] = 150; //this defines the npc danger detect range
            NPCID.Sets.AttackType[npc.type] = 1; //this is the attack type,  0 (throwing), 1 (shooting), or 2 (magic). 3 (melee)
            NPCID.Sets.AttackTime[npc.type] = 30; //this defines the npc attack speed
            NPCID.Sets.AttackAverageChance[npc.type] = 10;//this defines the npc atack chance
            NPCID.Sets.HatOffsetY[npc.type] = 4; //this defines the party hat position
            animationType = NPCID.Merchant;  //this copy the cyborg animation
        }
        public override string TownNPCName()     //Allows you to give this town NPC any name when it spawns
        {
            
                    return "Pad";
           
        }

        public override void SetChatButtons(ref string button, ref string button2)  //Allows you to set the text for the buttons that appear on this town NPC's chat window.
        {
            button = "Potion shop";   //this defines the buy button name
            button2 = "Loot Shop";
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool openShop) //Allows you to make something happen whenever a button is clicked on this town NPC's chat window. The firstButton parameter tells whether the first button or second button (button and button2 from SetChatButtons) was clicked. Set the shop parameter to true to open this NPC's shop.
        {

           
                ((Vendor)mod.GetNPC(Name)).otherShop = !firstButton;
                
                openShop = true;
            
      
            
        }

        public override void SetupShop(Chest shop, ref int nextSlot)       //Allows you to add items to this town NPC's shop. Add an item by setting the defaults of shop.item[nextSlot] then incrementing nextSlot.
        {
            if (!otherShop)
            {
                shop.item[nextSlot].SetDefaults(ItemID.ShinePotion);
                shop.item[nextSlot].value = 10000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.NightOwlPotion);
                shop.item[nextSlot].value = 10000;  //this is an example of how to add a modded item
                nextSlot++;
                if (DRGNModWorld.downedSerpent)   //this make so when the king slime is killed the town npc will sell this
                {
                    shop.item[nextSlot].SetDefaults(ItemID.IronskinPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.SwiftnessPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.FeatherfallPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.GillsPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.RegenerationPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.ObsidianSkinPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.WaterWalkingPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.ArcheryPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.GravitationPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.InvisibilityPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.MagicPowerPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.ManaRegenerationPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.BattlePotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.HunterPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.SpelunkerPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.ThornsPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.MiningPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.HeartreachPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.CalmingPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.BuilderPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.TitanPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.FlipperPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.SummoningPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.TrapsightPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.AmmoReservationPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.LifeforcePotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.EndurancePotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.RagePotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.InfernoPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.WrathPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.TeleportationPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.FishingPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.SonarPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.CratePotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.WarmthPotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.WormholePotion);
                    shop.item[nextSlot].value = 10000;
                    nextSlot++;
                }

            }
            else
            {
                shop.item[nextSlot].SetDefaults(ItemID.HermesBoots);
                shop.item[nextSlot].value = 10000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Aglet);
                shop.item[nextSlot].value = 10000; 
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.AnkletoftheWind);
                shop.item[nextSlot].value = 10000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.IceSkates);
                shop.item[nextSlot].value = 10000;
                nextSlot++;
                if (Main.hardMode)  
                {
                    shop.item[nextSlot].SetDefaults(ItemID.Vitamins);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.ArmorPolish);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.AdhesiveBandage);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Bezoar);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Nazar);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Megaphone);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.TrifoldMap);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.FastClock);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Blindfold);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                }
                if (NPC.downedGolemBoss)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.MoonCharm);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.SunStone);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.EyeoftheGolem);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.NeptunesShell);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Picksaw);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.MoonStone);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.BrokenHeroSword);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                }
                if (NPC.downedMartians)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.InfluxWaver);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.CosmicCarKey);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.LaserDrill);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.XenoStaff);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                }
                if (NPC.downedMoonlord)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.Meowmere);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.LastPrism);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Terrarian);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.StarWrath);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.FireworksLauncher);
                    shop.item[nextSlot].value = 100000;
                    nextSlot++;
                }
                   
                }


        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active || NPC.downedBoss1)
                {
                    return true;
                }

                
            }
            return false;
        }


        public override string GetChat()       //Allows you to give this town NPC a chat message when a player talks to it.
        {
            switch (Main.rand.Next(4))    //this are the messages when you talk to the npc
            {
                case 0:
                    return "Wanna buy some great loot";
                case 1:
                    return "Great day today right?";
                case 2:
                    return "I can't sell you those until you prove yourself";
                case 3:
                    return "tut tut no touching without buying";
                default:
                    return "Don't ask how they're here just deal with it";

            }
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)//  Allows you to determine the damage and knockback of this town NPC attack
        {
            damage = 40;  //npc damage
            knockback = 2f;   //npc knockback
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)  //Allows you to determine the cooldown between each of this town NPC's attack. The cooldown will be a number greater than or equal to the first parameter, and less then the sum of the two parameters.
        {
            cooldown = 5;

        }
        public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness) //Allows you to customize how this town NPC's weapon is drawn when this NPC is shooting (this NPC must have an attack type of 1). Scale is a multiplier for the item's drawing size, item is the ID of the item to be drawn, and closeness is how close the item should be drawn to the NPC.
        {
            scale = 1f;
            item = mod.ItemType("HellHornBow");
            closeness = 20;
        }
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)//Allows you to determine the projectile type of this town NPC's attack, and how long it takes for the projectile to actually appear
        {
            projType = mod.ProjectileType("SolarFlareProj");
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)//Allows you to determine the speed at which this town NPC throws a projectile when it attacks. Multiplier is the speed of the projectile, gravityCorrection is how much extra the projectile gets thrown upwards, and randomOffset allows you to randomize the projectile's velocity in a square centered around the original velocity
        {
            multiplier = 7f;


        }
    }
}