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
    public class VoidedWanderer : ModNPC
    {

        public override void SetDefaults()
        {

            npc.width = 34;
            npc.height = 46;
            npc.damage = 45;
            npc.defense = 15;
            npc.value = 8000;
            npc.lifeMax = 350;
            npc.aiStyle = 3;
            npc.knockBackResist = 0.3f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            Main.npcFrameCount[npc.type] = 3;

            animationType = NPCID.Zombie;
            banner = npc.type;
            bannerItem = ModContent.ItemType<VoidedWandererBanner>();

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {

            return (spawnInfo.spawnTileType == ModContent.TileType<VoidBrickTile>() || spawnInfo.spawnTileType == ModContent.TileType<VoidStoneTile>()) ? 0.5f : 0f;


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