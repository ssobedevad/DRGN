using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using DRGN.Items.Banners;
using DRGN.Items;
using DRGN.Tiles;

namespace DRGN.NPCs
{
    public class VoidedPitchfork : ModNPC
    {

        public override void SetDefaults()
        {

            npc.width = 60;
            npc.height = 60;
            npc.damage = 55;
            npc.defense = 12;
            npc.value = 9000;
            npc.lifeMax = 325;
            npc.aiStyle = 23;
            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            Main.npcFrameCount[npc.type] = 5;
            npc.lavaImmune = true;
            animationType = NPCID.CrimsonAxe;
            banner = npc.type;
            bannerItem = ModContent.ItemType<VoidedPitchforkBanner>();

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {

            return (spawnInfo.spawnTileType == ModContent.TileType<VoidBrickTile>() || spawnInfo.spawnTileType == ModContent.TileType<VoidStoneTile>()) ? 0.2f : 0f;


        }
        public override void NPCLoot()
        {
            if (NPC.downedMoonlord == true)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<VoidBrick>(), Main.rand.Next(3, 8));
                Item.NewItem(npc.getRect(), ModContent.ItemType<VoidStone>(), Main.rand.Next(3, 8));

            }
            else
            {
                Item.NewItem(npc.getRect(), ItemID.EbonstoneBrick, Main.rand.Next(3, 8));
                Item.NewItem(npc.getRect(), ItemID.DemoniteBrick, Main.rand.Next(3, 8));
            }

        }

    }
}