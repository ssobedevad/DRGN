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
    public class FlamingPitchfork : ModNPC
    {

        public override void SetDefaults()
        {

            npc.width = 60;
            npc.height = 60;
            npc.damage = 65;
            npc.defense = 16;
            npc.value = 10000;
            npc.lifeMax = 450;
            npc.aiStyle = 23;
            npc.knockBackResist = 0.1f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            Main.npcFrameCount[npc.type] = 5;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.lavaImmune = true;

            animationType = NPCID.CrimsonAxe;
            banner = npc.type;
            bannerItem = ModContent.ItemType<FlamingPitchforkBanner>();

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