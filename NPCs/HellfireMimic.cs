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
    public class HellfireMimic : ModNPC
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
            npc.lavaImmune = true;
            animationType = NPCID.BigMimicCrimson;



        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {

            return (spawnInfo.player.ZoneUnderworldHeight && Main.hardMode) ? 0.01f : 0f;


        }
        public override void NPCLoot()
        {
            int rand = Main.rand.Next(1, 6);
            
            if (rand == 1)
            {
                Item.NewItem(npc.getRect(), ItemID.DarkLance);
            }
            else if (rand == 2)
            {
                Item.NewItem(npc.getRect(), ItemID.Flamelash);
            }
            else if (rand == 3)
            {
                Item.NewItem(npc.getRect(), ItemID.FlowerofFire);
            }
            else if (rand == 4)
            {
                Item.NewItem(npc.getRect(), ItemID.Sunfury);
            }
            else 
            {
                Item.NewItem(npc.getRect(), ItemID.HellwingBow);
            }
            

            Item.NewItem(npc.getRect(), ItemID.GreaterHealingPotion, Main.rand.Next(5, 11));
            Item.NewItem(npc.getRect(), ItemID.GreaterManaPotion, Main.rand.Next(5, 11));






        }

    }
}