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
        

        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
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
               
                int target = Target();


                if (target != -1)
                {
                    move(target); }

            }
            
            
            
            

        }
        private void move(int Target)
        {

            float speed = 15f;
            Vector2 moveTo = Main.npc[Target].Center;
            Vector2 moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;
                projectile.timeLeft = 2;
                projectile.velocity = moveVel;
            }

        }








        private int Target()
        {
            int target = -1;
            int targetMag = 1000;
            for (int whichNpc = 0; whichNpc < 200; whichNpc++)
            {
                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {
                    float whichNpcXpos = Main.npc[whichNpc].Center.X;
                    float whichNpcYpos = Main.npc[whichNpc].Center.Y;
                    float DistanceProjtoNpc = Math.Abs(this.projectile.position.X + (float)(this.projectile.width / 2) - whichNpcXpos) + Math.Abs(this.projectile.position.Y + (float)(this.projectile.height / 2) - whichNpcYpos);
                    if (DistanceProjtoNpc < targetMag)
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;
                       

                    }
                }
            }
            return target;


        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }






    }
}
