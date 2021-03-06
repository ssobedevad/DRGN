﻿using System;
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
    public class Bloodreaper : ModNPC
    {
        public override void SetDefaults()
        {
            npc.lifeMax = 1500;
            npc.height = 64;
            npc.width = 64;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.aiStyle = 2;
            npc.damage = 250;
            npc.defense = 50;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.value = 10000;
            npc.knockBackResist = 0.1f;
            banner = npc.type;
            bannerItem = ModContent.ItemType<BloodReaperBanner>();

        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (DRGNModWorld.downedDragon) { if (Main.expertMode) { npc.lifeMax = 17000; npc.life = 17000; } else { npc.lifeMax = 8500; npc.life = 8500; } }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode == true)
            {
                return spawnInfo.spawnTileType == mod.TileType("DragonBrick") ? DRGNModWorld.downedDragon ? 1f : 0.05f : 0f;
            }
            else { return 0f; }
        }
        public override void AI()
        {
            npc.rotation = npc.velocity.ToRotation() + 1.57f;
        }
        public override void NPCLoot()
        {
            if (DRGNModWorld.downedDragon)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("DragonScale"), Main.rand.Next(18, 30));
                if (Main.rand.Next(3) == 0)
                { Item.NewItem(npc.getRect(), mod.ItemType("SolariumOre"), Main.rand.Next(8, 10)); }
            }
        }
    }
}