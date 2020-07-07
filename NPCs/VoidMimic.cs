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
    public class VoidMimic : ModNPC
    {

        public override void SetDefaults()
        {

            npc.width = 42;
            npc.height = 48;
            npc.damage = 180;
            npc.defense = 34;
            npc.value = 80000;
            npc.lifeMax = 12000;
            npc.aiStyle = 87;
            npc.knockBackResist = 0.01f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            Main.npcFrameCount[npc.type] = 14;
            npc.lavaImmune = true;
            animationType = NPCID.BigMimicCrimson;



        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {

            return (spawnInfo.player.GetModPlayer<DRGNPlayer>().VoidBiome) ? 0.05f : 0f;


        }
        public override void NPCLoot()
        {


            Item.NewItem(npc.getRect(), ModContent.ItemType<VoidKey>());
            Item.NewItem(npc.getRect(), ItemID.GreaterHealingPotion, Main.rand.Next(5, 11));
            Item.NewItem(npc.getRect(), ItemID.GreaterManaPotion, Main.rand.Next(5, 11));






        }

    }
}