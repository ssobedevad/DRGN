using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace DRGN.Projectiles
{
    public class DragonBladeEx : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 1;


            projectile.tileCollide = true;

        }

        public override void AI()
        {
            projectile.ai[1] += 1;
            if (projectile.ai[1] >= 12)
            {

                int target = Target();


                if (target != -1)
                {
                    move(target);
                }

            }

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);



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
                projectile.velocity = projectile.velocity + moveVel / 20f;
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
