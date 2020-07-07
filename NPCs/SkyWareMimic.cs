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
    public class SkyWareMimic : ModNPC
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

            return (spawnInfo.player.ZoneSkyHeight && Main.hardMode) ? 0.01f : 0f;


        }
        public override void NPCLoot()
        {
            int rand = Main.rand.Next(1, 4);
            int rand2 = Main.rand.Next(1, 3);
            if (rand == 1)
            {
                Item.NewItem(npc.getRect(), ItemID.ShinyRedBalloon);
            }
            else if (rand == 2)
            {
                Item.NewItem(npc.getRect(), ItemID.Starfury);
            }
            else
            {
                Item.NewItem(npc.getRect(), ItemID.LuckyHorseshoe);
            }
            if(rand2 == 1) { Item.NewItem(npc.getRect(), ItemID.SkyMill); }
            


            Item.NewItem(npc.getRect(), ItemID.GreaterHealingPotion, Main.rand.Next(5, 11));
            Item.NewItem(npc.getRect(), ItemID.GreaterManaPotion, Main.rand.Next(5, 11));






        }

    }
}