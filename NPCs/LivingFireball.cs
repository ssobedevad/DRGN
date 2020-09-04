using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using DRGN.Items.Banners;
using DRGN.Items;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.NPCs
{
    public class LivingFireball : ModNPC
    {

        public override void SetDefaults()
        {
            npc.width = 28;
            npc.height = 28;
            npc.damage = 25;
            npc.defense = 12;
            npc.value = 3000;
            npc.lifeMax = 600;
            npc.aiStyle = -1;
            npc.knockBackResist = 0.25f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lavaImmune = true;
        }
        public override void AI()
        {
            npc.TargetClosest(true);            
            Vector2 Diff = Main.player[npc.target].Center - npc.Center;            
            float Magnitude = Diff.Length();
            float Speed = 12f;
            Magnitude = Speed / Magnitude;
            Diff *= Magnitude;
            npc.velocity = (npc.velocity * 90f  + Diff )/91f;           
            npc.rotation = (float)Math.Atan2(Diff.Y,Diff.X) - 1.57f;
            int Dustid = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Fire, npc.velocity.X * -0.4f, npc.velocity.Y * -0.4f, 0, Color.White, 1f);
            Main.dust[Dustid].noGravity = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.GetModPlayer<DRGNPlayer>().DragonBiome) ? 1f : 0f;
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), mod.ItemType("FlareCrystal"), Main.rand.Next(2, 8));
            Item.NewItem(npc.getRect(), mod.ItemType("AshenWood"), Main.rand.Next(2, 8));
        }

    }
}