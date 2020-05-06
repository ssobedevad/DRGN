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
            npc.height = 26;
            npc.width = 52;
            npc.aiStyle = -1;
            npc.damage = 450;
            npc.defense = 50;
            npc.scale = 1.5f;
            npc.value = 10000;
            npc.knockBackResist = 0.1f;
            

        }
        private void Target()
        {
           
            if (Main.player[npc.target].active)
            {
                player = Main.player[npc.target];
            }
            else { npc.target += 1; }
            if (npc.target == 255) { npc.target = 0; }
        }
        public override void AI()
        {
            npc.target = 0;
            Target();
            if (!player.active) { return; }
            npc.spriteDirection = npc.direction;
            if (stuck >= 50) 
            {
                 npc.noTileCollide = true;
                 npc.noGravity = true;
                  npc.velocity.Y = -5; 
                 if (npc.Center.Y < player.Center.Y - 50) { stuck = 0; }
            }
            
            else  {
                npc.noTileCollide = false; npc.noGravity = false;
                if (player.Center.X -50 > npc.Center.X) { npc.velocity.X = 5; npc.direction = -1; }
                else if (player.Center.X + 50 < npc.Center.X) { npc.velocity.X = -5; npc.direction = 1; }
                else if (player.Center.Y > npc.Center.Y) { npc.velocity.Y = 2; }
                else if (player.Center.Y < npc.Center.Y) { npc.velocity.Y = -2; }
                if (Main.rand.Next(0,200)==1) { Spit(); }
                if (npc.collideX) { npc.velocity.Y = -3; stuck += 1; }
                else { stuck = 0; }
            }
             
            

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
            int frame = (int)(npc.frameCounter / 5.0);  // only change frame every second tick
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