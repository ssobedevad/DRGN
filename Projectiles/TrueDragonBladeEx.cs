using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class TrueDragonBladeEx : ModProjectile
    {
        private Vector2 target = new Vector2(0, 0);

        public int whichNpc;
        public int whichNPCReal;
        private Vector2 moveVel;
        private int targetMag = 10000;
        private float speed;
        private Vector2 moveTo;

        public override void SetDefaults()
        {

            projectile.height = 8;
            projectile.width = 8;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 1;


            projectile.tileCollide = true;
            projectile.ai[1] = 0;
        }
        public override void AI()
        {
            projectile.ai[1] += 1;
            if (projectile.ai[1] >= 20)
            {
                targetMag = 10000;
                Target();


                if (target != new Vector2(0, 0))
                {
                    move(); }

            }
            
            
            
            

        }
        private void move()
        {

            speed = 15f;
            moveTo = target;
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
                        target = Main.npc[whichNpc].Center;
                        whichNPCReal = whichNpc;

                    }
                }
            }


        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }






    }
}
