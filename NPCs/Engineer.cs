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
            

            

            button2 = "Disassemble";
            
            //this defines the buy button name
            
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool openShop) //Allows you to make something happen whenever a button is clicked on this town NPC's chat window. The firstButton parameter tells whether the first button or second button (button and button2 from SetChatButtons) was clicked. Set the shop parameter to true to open this NPC's shop.
        {

            if (firstButton)
            {
                openShop = true;   //so when you click on buy button opens the shop
            }
            else {
                Main.playerInventory = true;
                // remove the chat window...
                Main.npcChatText = "";
                // and start an instance of our UIState.
                ModContent.GetInstance<DRGN>()._DisUI.SetState(ModContent.GetInstance<DRGN>().disassembleUI);
            }
           
            
        }
        

        public override void SetupShop(Chest shop, ref int nextSlot)       //Allows you to add items to this town NPC's shop. Add an item by setting the defaults of shop.item[nextSlot] then incrementing nextSlot.
        {
            Player player = Main.LocalPlayer;
            if (NPC.downedBoss1)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("Compressor"));
                shop.item[nextSlot].value = 10000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("MetalloidConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }
            if (NPC.downedBoss3)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("GoldenConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }
            if (DRGNModWorld.downedIceFish)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("IcyConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }
            if (NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("PlantenConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }
            if (NPC.downedMoonlord)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("LunarConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }
            if (DRGNModWorld.downedDragonFly)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("InsectiumConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }
            if (DRGNModWorld.downedDragon)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("FlariumConverter"));
                shop.item[nextSlot].value = 100;
                nextSlot++;
            }
            if (DRGNModWorld.downedVoidSnake)
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

            int Quest = Main.rand.Next(1, 5);   //this are the messages when you talk to the npc

            

          
            if (Quest == 0)
            {
                return "I can turn some of your unwanted weapons into spare parts";
            }
            else if (Quest == 1)
            {
                return "I do love my heavy machinary";
            }
            else if (Quest == 2)
            {
                return "You can repair anything with an old broom handle";
            }
            else if(Quest == 3)
            {
                return "Im a man of few words";
            }
            else if(Quest == 4)
            {
                return "Yo wassup";
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