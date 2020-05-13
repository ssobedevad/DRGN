using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
namespace DRGN.NPCs
{
    public class VoidEye : ModNPC
    {
        private Player player;
        private Vector2 ProjVel;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Eye");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            
            npc.damage = 120;
            npc.height = 20;
            npc.width = 36;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lifeMax = 50000;
            npc.knockBackResist = 0f;
        }
        private void Target()
        {

            npc.TargetClosest(false);
            player = Main.player[npc.target];
        }
        public override void AI()
        {
            Target();
            Vector2 moveVel = (player.Center - npc.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude >= 1800) { player.AddBuff(mod.BuffType("Webbed"), 60); }
            if (NPC.AnyNPCs(mod.NPCType("VoidSnakeHead")) == false) { npc.active = false; }else { npc.timeLeft = 1800; }
            if (Main.rand.Next(0,150) == 1)
            {
                Shoot();
               
             Projectile.NewProjectile(npc.Center.X , npc.Center.Y, ProjVel.X, ProjVel.Y, mod.ProjectileType("VoidLazer"), 150, 0);
                npc.frame.Y = 20;
            }
            if (Main.rand.Next(20) == 1) { npc.frame.Y = 0; }

        }
        private void Shoot()
        {
            float speed = 30f;
            Vector2 moveTo = player.Center;
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            
                move *= speed / magnitude;
            
            
            
            ProjVel = move;

        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
    }
}