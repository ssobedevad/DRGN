using Terraria;
using System;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class ExCaliburrProj : ModProjectile
    {

        private int target;
        private bool reTarget;
        public int whichNpc;
        private Vector2 moveVel;
        private int targetMag = 1000;
        private float speed;
        private Vector2 moveTo;
        private bool ricochet;
        private int JustHit;
        public override void SetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 5;
            projectile.tileCollide = true;
            ricochet = false;
            reTarget = false;
        }
        public override void AI()
        {
            if (ricochet == true)
            {
                if (reTarget == true || Main.npc[target].active == false)
                {
                    Target();
                }
                if (target == -1)
                { projectile.active = false; return; }
                else { move(); }


            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)

        {

            ricochet = true;
            JustHit = target.whoAmI;
            projectile.tileCollide = false;
            reTarget = true;
        }

        private void move()
        {

            speed = 15f;
            moveTo = Main.npc[target].Center;
            moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;
                
               
            }
            else 
            {
                reTarget = true;
                JustHit = target;

            }
            projectile.velocity = moveVel;

        }
        







        private void Target()
        {
            target = -1;
            targetMag = 1000;
            for (whichNpc = 0; whichNpc < 200; whichNpc++)
            {
                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {
                    float whichNpcXpos = Main.npc[whichNpc].Center.X;
                    float whichNpcYpos = Main.npc[whichNpc].Center.Y;
                    float DistanceProjtoNpc = Math.Abs(this.projectile.position.X + (float)(this.projectile.width / 2) - whichNpcXpos) + Math.Abs(this.projectile.position.Y + (float)(this.projectile.height / 2) - whichNpcYpos);
                    if (DistanceProjtoNpc < targetMag && whichNpc != JustHit)
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;

                    }
                }
            }
            reTarget = false;


        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

    




    }
}
