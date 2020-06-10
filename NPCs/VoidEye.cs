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
        private int shootCD;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Eye");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.netUpdate = true;
            npc.netAlways = true;
            npc.damage = 100;
            npc.height = 20;
            npc.width = 36;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.dontCountMe = true;
            npc.lifeMax = 50000;
            npc.knockBackResist = 0f;
            npc.active = true;
            shootCD = 200;
        }
        private void Target()
        {

            npc.TargetClosest(false);
            player = Main.player[npc.target];
        }
        public override void AI()
        {
            Target();
            if (shootCD > 0) { shootCD -= Main.rand.Next(0,10); }
            Vector2 moveVel = (player.Center - npc.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude >= 1800) { player.AddBuff(mod.BuffType("Webbed"), 60); }
            if (NPC.AnyNPCs(mod.NPCType("VoidSnakeHead")) == false) { npc.active = false; }else { npc.timeLeft = 1800; }
            if (shootCD <= 0)
            {
                Shoot();
               
             Projectile.NewProjectile(npc.Center.X , npc.Center.Y, ProjVel.X, ProjVel.Y, mod.ProjectileType("VoidLazer"), npc.damage/2, 0);
                npc.frame.Y = 20;
                shootCD = 200;
            }
            if (Main.rand.Next(20) == 1) { npc.frame.Y = 0; }

        }
        private void Shoot()
        {
            float speed = 10f;
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
       public override bool CheckActive()
       { return false; }
        public override bool CheckDead()
        { return false; }


    }
}