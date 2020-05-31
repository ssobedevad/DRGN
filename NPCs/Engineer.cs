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
    public class Engineer : ModNPC
    {
    
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Engineer");
            

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
            switch (WorldGen.genRand.Next(1))
            {
                case 0:
                    return "Terry";
                default:
                    return "Bill";
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)  //Allows you to set the text for the buttons that appear on this town NPC's chat window.
        {
            button = "Shop";
            if (Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuestTime >= 36000) { button2 = " New Quest"; }
            else
            {
                button2 = "Quest";
            }  //this defines the buy button name
            
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool openShop) //Allows you to make something happen whenever a button is clicked on this town NPC's chat window. The firstButton parameter tells whether the first button or second button (button and button2 from SetChatButtons) was clicked. Set the shop parameter to true to open this NPC's shop.
        {

            if (firstButton)
            {
                openShop = true;   //so when you click on buy button opens the shop
            }
            else if (Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuest == -1 || Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuestTime >= 36000)
            {
                Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuest = Main.rand.Next(Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuestLowerLimit, 2 * Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuestTier);
                int Quest = Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuest;
                if (Quest == -1)
                {
                    Main.npcChatText = "Ive got some spare parts if you would do a mission for me";
                }
                if (Quest == 0)
                {
                    Main.npcChatText = "If you could fetch me some expensive metals I would happily repay you \n 5 gold bars will do";
                }
                else if (Quest == 1)
                {
                    Main.npcChatText = "I'm really short on clay pots I don't know why but another 5 would make this place look much better";
                }
                else if (Quest == 2)
                {
                    Main.npcChatText = "That big eye really scares me if you were to bring me a trophy it would reassure me greatly";
                }
                else if (Quest == 3)
                {
                    Main.npcChatText = "In the jungle there are some big scary green snakes a sample of their flesh would support my research";
                }
                else if (Quest == 4)
                {
                    Main.npcChatText = "Those skeletons that crawl around in the dungeons are very valuable a small pile of bones would add greatly to this room";
                }
                else if (Quest == 5)
                {
                    Main.npcChatText = "Ive heard it gets rather hot as you dig down and it gets a bit chilly up here you know";
                }
                else if (Quest == 6)
                {
                    Main.npcChatText = "Ive heard tales of that big ice monster but never seen it before a small piece would serve as a nice trophy";
                }
                else if (Quest == 7)
                {
                    Main.npcChatText = "Those big metal monsters sell well I could return you a small fortune for a couple of bars";
                }
                else if (Quest == 8)
                {
                    Main.npcChatText = "Theres a big brown temple full of green people, its strange but youve gotta live with it, I wouldn't mind a bit of their walls though";
                }
                else if (Quest == 9)
                {
                    Main.npcChatText = "Those blue mysterious bars that the mushroom man is made from look shiny id love to take a look at some";
                }
                else if (Quest == 10)
                {
                    Main.npcChatText = "I guess you've heard of the big sky snakes they drop some nice crystals";
                }
                else if (Quest == 11)
                {
                    Main.npcChatText = "Id like to take a look at some of those lunar bars you have there";
                }
                else if (Quest == 12)
                {
                    Main.npcChatText = "A few bits of dragonfly wing wouldnt go unnoticed";
                }
                else if (Quest == 13)
                {
                    Main.npcChatText = "Ive heard that the essence of galaxies works well for lighting around here";
                }
                else if (Quest == 14)
                {
                    Main.npcChatText = "In the depths of hell its warm and only firey metals can do the same for heating up here";
                }
                else if (Quest == 15)
                {
                    Main.npcChatText = "Some of those big dragons drop scales that would look nice on the end of my hat";
                }
                else if (Quest == 16)
                {
                    Main.npcChatText = "Some of those galactite bars would work very well for making energy around here";
                }
                else if (Quest == 17)
                {
                    Main.npcChatText = "I could do with a better pickaxe maybe one that mines like the void";
                }

            }
            else
            {
                Player player = Main.LocalPlayer;
                int Quest = player.GetModPlayer<DRGNPlayer>().engineerQuest;
                for (int i = 0; i < 58; i++)
                {
                    if (Quest == 0)
                    {
                        if (player.inventory[i].type == ItemID.GoldBar && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0,true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }

                    }
                    else if (Quest == 1)
                    {
                        if (player.inventory[i].type == ItemID.ClayPot && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                        
                    }
                    else if (Quest == 2)
                    {
                        if (player.inventory[i].type == ItemID.EyeofCthulhuTrophy) { player.inventory[i].stack -= 1; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                       
                    }
                    else if (Quest == 3)
                    {
                        if (player.inventory[i].type == mod.ItemType("ToxicFlesh") && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                        
                    }
                    else if (Quest == 4)
                    {
                        if (player.inventory[i].type == ItemID.Bone && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                    }
                    else if (Quest == 5)
                    {
                        if (player.ZoneUnderworldHeight) { player.GetModPlayer<DRGNPlayer>().engineerQuest = -1;}

                    }
                    else if (Quest == 6)
                    {
                        if (player.inventory[i].type == mod.ItemType("GlacialShard") && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                    }
                    else if (Quest == 7)
                    {
                        if (player.inventory[i].type == ItemID.HallowedBar && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                        
                    }
                    else if (Quest == 8)
                    {
                        if (player.inventory[i].type == ItemID.LihzahrdBrick && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                       
                    }
                    else if (Quest == 9)
                    {
                        if (player.inventory[i].type == ItemID.ShroomiteBar && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                        
                    }
                    else if (Quest == 10)
                    {
                        if (player.inventory[i].type == mod.ItemType("LunarFragment") && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                       
                    }
                    else if (Quest == 11)
                    {
                        if (player.inventory[i].type == ItemID.LunarBar && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                        
                    }
                    else if (Quest == 12)
                    {
                        if (player.inventory[i].type == mod.ItemType("DragonFlyWing") && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                        
                    }
                    else if (Quest == 13)
                    {
                        if(player.inventory[i].type == mod.ItemType("GalacticEssence") && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                       
                    }
                    else if (Quest == 14)
                    {
                        if(player.inventory[i].type == mod.ItemType("SolariumBar") && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                       
                    }
                    else if (Quest == 15)
                    {
                        if(player.inventory[i].type == mod.ItemType("DragonScale") && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                        
                    }
                    else if (Quest == 16)
                    {
                        if(player.inventory[i].type == mod.ItemType("GalacticaBar") && player.inventory[i].stack >= 5) { player.inventory[i].stack -= 5; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                       
                    }
                    else if (Quest == 17)
                    {
                        if(player.inventory[i].type == mod.ItemType("VoidPick") ) { player.inventory[i].stack -= 1; if (player.inventory[i].stack <= 0) { player.inventory[i].SetDefaults(0, true); } player.GetModPlayer<DRGNPlayer>().engineerQuest = -1; }
                        
                    }
                    
                }
                if (player.GetModPlayer<DRGNPlayer>().engineerQuest == -1) { player.GetModPlayer<DRGNPlayer>().engineerQuestNum += 1; player.QuickSpawnItem(mod.ItemType("EngineerPartsBag")); }






            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)       //Allows you to add items to this town NPC's shop. Add an item by setting the defaults of shop.item[nextSlot] then incrementing nextSlot.
        {
            Player player = Main.LocalPlayer;
            if (player.GetModPlayer<DRGNPlayer>().engineerQuestNum > 10 && Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuestTier >= 2)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("Compressor"));
                shop.item[nextSlot].value = 10000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("MetalloidConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }
            if (player.GetModPlayer<DRGNPlayer>().engineerQuestNum > 20 && Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuestTier >= 3)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("GoldenConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }
            if (player.GetModPlayer<DRGNPlayer>().engineerQuestNum > 35 && Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuestTier >= 4)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("IcyConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }
            if (player.GetModPlayer<DRGNPlayer>().engineerQuestNum > 50 && Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuestTier >= 5)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("PlantenConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }
            if (player.GetModPlayer<DRGNPlayer>().engineerQuestNum > 75 && Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuestTier >= 6)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("LunarConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }
            if (player.GetModPlayer<DRGNPlayer>().engineerQuestNum > 100 && Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuestTier >= 7)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("InsectiumConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }
            if (player.GetModPlayer<DRGNPlayer>().engineerQuestNum > 125 && Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuestTier >= 8)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("FlariumConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }
            if (player.GetModPlayer<DRGNPlayer>().engineerQuestNum > 200 && Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuestTier >= 9)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("VoidConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }




        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            
                    return true;
                


            
        }
        public override string GetChat()       //Allows you to give this town NPC a chat message when a player talks to it.
        {

            int Quest = (Main.LocalPlayer.GetModPlayer<DRGNPlayer>().engineerQuest);   //this are the messages when you talk to the npc

            

            if (Quest == -1)
            {
                return "Ive got some spare parts if you would do a mission for me";
            }
            if (Quest == 0)
            {
                return "If you could fetch me some expensive metals I would happily repay you \n 5 gold bars will do";
            }
            else if (Quest == 1)
            {
                return "I'm really short on clay pots I don't know why but another 5 would make this place look much better";
            }
            else if (Quest == 2)
            {
                return "That big eye really scares me if you were to bring me a trophy it would reassure me greatly";
            }
            else if(Quest == 3)
            {
                return "In the jungle there are some big scary green snakes a sample of their flesh would support my research";
            }
            else if(Quest == 4)
            {
                return "Those skeletons that crawl around in the dungeons are very valuable a small pile of bones would add greatly to this room";
            }
            else if(Quest == 5)
            {
                return "Ive heard it gets rather hot as you dig down and it gets a bit chilly up here you know";
            }
            else if(Quest == 6)
            {
                return "Ive heard tales of that big ice monster but never seen it before a small piece would serve as a nice trophy";
            }
            else if(Quest == 7)
            {
                return "Those big metal monsters sell well I could return you a small fortune for a couple of bars";
            }
            else if(Quest == 8)
            {
                return "Theres a big brown temple full of green people, its strange but youve gotta live with it, I wouldn't mind a bit of their walls though";
            }
            else if(Quest == 9)
            {
                return "Those blue mysterious bars that the mushroom man is made from look shiny id love to take a look at some";
            }
            else if(Quest == 10)
            {
                return "I guess you've heard of the big sky snakes they drop some nice crystals";
            }
            else if(Quest == 11)
            {
                return "Id like to take a look at some of those lunar bars you have there";
            }
            else if(Quest == 12)
            {
                return "A few bits of dragonfly wing wouldnt go unnoticed";
            }
            else if(Quest == 13)
            {
                return "Ive heard that the essence of galaxies works well for lighting around here";
            }
            else if(Quest == 14)
            {
                return "In the depths of hell its warm and only firey metals can do the same for heating up here";
            }
            else if(Quest == 15)
            {
                return "Some of those big dragons drop scales that would look nice on the end of my hat";
            }
            else if(Quest == 16)
            {
                return "Some of those galactite bars would work very well for making energy around here";
            }
            else if(Quest == 17)
            {
                return "I could do with a better pickaxe maybe one that mines like the void";
            }
            else { return ""; }
                




            
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)//  Allows you to determine the damage and knockback of this town NPC attack
        {
            damage = 60;  //npc damage
            knockback = 2f;   //npc knockback
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)  //Allows you to determine the cooldown between each of this town NPC's attack. The cooldown will be a number greater than or equal to the first parameter, and less then the sum of the two parameters.
        {
            cooldown = 5;
            
        }
        public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness) //Allows you to customize how this town NPC's weapon is drawn when this NPC is shooting (this NPC must have an attack type of 1). Scale is a multiplier for the item's drawing size, item is the ID of the item to be drawn, and closeness is how close the item should be drawn to the NPC.
          {
              scale = 1f;
              item = mod.ItemType("EngineerRifleTier8");
              closeness = 20;
          }
    public override void TownNPCAttackProj(ref int projType, ref int attackDelay)//Allows you to determine the projectile type of this town NPC's attack, and how long it takes for the projectile to actually appear
    {
        projType = mod.ProjectileType("EngineerVoidPhantomBlade");
            attackDelay = 1;
    }

    public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)//Allows you to determine the speed at which this town NPC throws a projectile when it attacks. Multiplier is the speed of the projectile, gravityCorrection is how much extra the projectile gets thrown upwards, and randomOffset allows you to randomize the projectile's velocity in a square centered around the original velocity
    {
        multiplier = 16f;
        

    }
}
}