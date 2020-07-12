using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using DRGN.Items.Banners;
using DRGN.Items;

namespace DRGN.NPCs
{
    public class JungleMimic : ModNPC
    {

        public override void SetDefaults()
        {

            npc.width = 42;
            npc.height = 48;
            npc.damage = 90;
            npc.defense = 34;
            npc.value = 30000;
            npc.lifeMax = 3500;
            npc.aiStyle = 87;
            npc.knockBackResist = 0.1f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            Main.npcFrameCount[npc.type] = 14;
            
            animationType = NPCID.BigMimicCrimson;
            
            

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {

            return (spawnInfo.player.ZoneJungle && Main.hardMode) ? 0.01f : 0f;


        }
        public override void NPCLoot()
        {
            int rand = Main.rand.Next(1, 8);
            int rand2 = Main.rand.Next(1, 4);
            if (rand == 1)
            {
                Item.NewItem(npc.getRect(),ItemID.AnkletoftheWind);
            }
            else if (rand == 2)
            {
                Item.NewItem(npc.getRect(), ItemID.FeralClaws);
            }
            else if(rand == 3)
            {
                Item.NewItem(npc.getRect(), ItemID.StaffofRegrowth);
            }
            else if (rand == 4)
            {
                Item.NewItem(npc.getRect(), ItemID.Boomstick);
            }
            else if (rand == 5)
            {
                Item.NewItem(npc.getRect(), ItemID.FlowerBoots);
            }
            else if (rand == 6)
            {
                Item.NewItem(npc.getRect(), ItemID.FiberglassFishingPole);
            }
            else
            {
                Item.NewItem(npc.getRect(), ItemID.Seaweed);
            }
            if (rand2 == 1)
            {
                Item.NewItem(npc.getRect(), ItemID.HoneyDispenser);
            }
            else if (rand2 == 2)
            {
                Item.NewItem(npc.getRect(), ItemID.LivingMahoganyWand);
            }
            else 
            {
                Item.NewItem(npc.getRect(), ItemID.LivingMahoganyLeafWand);
            }
            Item.NewItem(npc.getRect(), ItemID.GreaterHealingPotion, Main.rand.Next(5,11));
            Item.NewItem(npc.getRect(), ItemID.GreaterManaPotion, Main.rand.Next(5, 11));






        }

    }
}