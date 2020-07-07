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

namespace DRGN.NPCs
{
    public class TopazSlime : ModNPC
    {

        public override void SetStaticDefaults()
        {

            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 75;
            npc.height = 30;
            npc.width = 36;
            npc.aiStyle = 1;
            npc.damage = 18;
            npc.defense = 8;
            animationType = NPCID.BlueSlime;
            npc.value = 200;
            npc.knockBackResist = 0.3f;
            banner = npc.type;
            bannerItem = ModContent.ItemType<TopazSlimeBanner>();

        }




        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.ZoneRockLayerHeight ? 0.1f : 0f;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ItemID.Topaz, Main.rand.Next(3, 8));

        }

    }
}