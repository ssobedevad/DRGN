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

namespace DRGN.NPCs
{
    public class VoidCrawler : ModNPC
    {
        private int stuck;
        private Player player;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Crawler");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 8500;
            npc.height = 52;
            npc.width = 104;
            npc.aiStyle = 3;
            npc.damage = 450;
            npc.defense = 50;
            npc.scale = 1.5f;
            npc.value = 10000;
            npc.knockBackResist = 0.1f;
            

        }
        private void Target()
        {
            npc.TargetClosest(true);
            player = Main.player[npc.target];
            if (!player.active || player.dead) { npc.target = -1; }
        }
        public override void AI()
        { if (npc.target > -1 && Main.rand.Next(0, 400) == 1) { Spit(); npc.spriteDirection = npc.direction; }
         }
        
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode == true)
            {
                return Main.tile[(spawnInfo.spawnTileX), (spawnInfo.spawnTileY)].type == mod.TileType("VoidBrickTile") ? 200f : 0f;
            }
            else { return 0f; }
        }
       

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1;
            npc.frameCounter %= 15;  // number of frames * tick count
            int frame = (int)(npc.frameCounter / 10.0);  // only change frame every second tick
            if (frame >= Main.npcFrameCount[npc.type]) frame = 0;  // check for final frame
            npc.frame.Y = frame * frameHeight;

        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), mod.ItemType("VoidSilk"), 5);
            if (Main.rand.Next(3) == 0)
            { Item.NewItem(npc.getRect(), mod.ItemType("VoidStone"), 5); }
        }
        private void Spit()
        {
            Vector2 target = player.Center;
            Vector2 spitVel = (target - npc.Center);
            float magnitude = Magnitude(spitVel);
            spitVel *= 10 / magnitude;
            Projectile.NewProjectile(npc.Center.X,npc.Center.Y, spitVel.X,spitVel.Y, mod.ProjectileType("VoidWeb"), 50,0);
        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
    }
}