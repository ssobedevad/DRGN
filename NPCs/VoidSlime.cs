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
using DRGN.Items.Banners;
using DRGN.Tiles;
using DRGN.Items;

namespace DRGN.NPCs
{
    public class VoidSlime : ModNPC
    {

        public override void SetStaticDefaults()
        {

            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 250;
            npc.height = 30;
            npc.width = 36;
            npc.aiStyle = 1;
            npc.damage = 40;
            npc.defense = 18;
            animationType = NPCID.BlueSlime;
            npc.value = 8000;
            npc.knockBackResist = 0.3f;
            banner = npc.type;
            bannerItem = ModContent.ItemType<VoidSlimeBanner>();

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