using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class TrueUnstableMeteor : ModProjectile
    {


        private NPC target;
        public int targetNum;
        public int whichNpc;
        private Vector2 moveVel;
        private int targetMag = 1000;
        private float speed;
        private Vector2 moveTo;

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
           
            projectile.light = 8f;



        }
        public override void AI()
        {
           
           
            Target();
            if(targetNum != -1) { move(); }
            if (Main.rand.Next(2) == 0)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 74, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 1.8f);
                Main.dust[DustID].noGravity = true;
            }
        }
        private void move()
        {

            speed = 15f;
            moveTo = target.Center;
            moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;
                projectile.timeLeft = 2;
                projectile.velocity = moveVel;
            }

        }








        private void Target()
        {
            targetMag = 1000;
            targetNum = -1;

            for (whichNpc = 0; whichNpc < 200; whichNpc++)
            {
                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {
                    float whichNpcXpos = Main.npc[whichNpc].Center.X;
                    float whichNpcYpos = Main.npc[whichNpc].Center.Y;
                    float DistanceProjtoNpc = Math.Abs(this.projectile.position.X + (float)(this.projectile.width / 2) - whichNpcXpos) + Math.Abs(this.projectile.position.Y + (float)(this.projectile.height / 2) - whichNpcYpos);
                    if (DistanceProjtoNpc < targetMag)
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = Main.npc[whichNpc];
                        targetNum = whichNpc;

                    }
                }
            }


        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {
            for (int i = 0; i < Main.rand.Next(3, 6); ++i)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 800, Main.rand.Next(-3, 3), 10, mod.ProjectileType("TrueUnstableMeteorShatter"), projectile.damage, projectile.knockBack, Main.myPlayer, projectile.Center.X,projectile.Center.Y);
            }
            

            base.OnHitNPC(target, damage, knockBack, crit);
        }
       

       
    }
}