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
    public class RockSlime : ModNPC
    {

        public override void SetStaticDefaults()
        {
            
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 55;
            npc.height = 24;
            npc.width = 32;
            npc.aiStyle = 1;
            npc.damage = 15;
            npc.defense = 6;
            animationType = NPCID.BlueSlime;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 100;
            npc.knockBackResist = 0.3f;
            banner = npc.type;
            bannerItem = ModContent.ItemType<RockSlimeBanner>();

        }




        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.ZoneOverworldHeight? 0.1f : 0f;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ItemID.StoneBlock, Main.rand.Next(1,5));
            if (Main.rand.Next(2) == 0)
            { Item.NewItem(npc.getRect(), ItemID.ActiveStoneBlock); }
        }

    }
}