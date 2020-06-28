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
    public class HellstoneSlime : ModNPC
    {

        public override void SetStaticDefaults()
        {

            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 350;
            npc.height = 30;
            npc.width = 36;
            npc.aiStyle = 1;
            npc.damage = 45;
            npc.defense = 22;
            animationType = NPCID.BlueSlime;
            npc.value = 10000;
            npc.lavaImmune = true;
            npc.knockBackResist = 0.1f;
            banner = npc.type;
            bannerItem = ModContent.ItemType<HellstoneSlimeBanner>();

        }
        public override void AI()
        {
            int Dustid = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Fire, 0, 0, 120, default(Color), 1f);
            Main.dust[Dustid].noGravity = true;
        }




        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {

            return spawnInfo.player.ZoneUnderworldHeight ? 0.1f : 0f;


        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ItemID.Obsidian, Main.rand.Next(5, 20));
            Item.NewItem(npc.getRect(), ItemID.Hellstone, Main.rand.Next(5, 20));

        }

    }
}