using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
namespace DRGN.Projectiles
{
    public class VoidedArrow : ModProjectile
    {




        public override void SetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 5;
            projectile.tileCollide = true;

            projectile.localAI[0] = -2;
        }
        public override void AI()
        {
            if (projectile.localAI[0] > -2)
            {
                if (projectile.localAI[0] == -1 || Main.npc[(int)projectile.localAI[0]].active == false)
                {
                    Target();
                }
                if (projectile.localAI[0] == -1)
                { projectile.active = false; return; }
                else { move(); }


            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)

        {


            Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("VoidedExplosion"), projectile.damage, 0f, projectile.owner);
            projectile.ai[1] = target.whoAmI;
            projectile.tileCollide = false;
            projectile.localAI[0] = -1;
            projectile.hide = true;
        }

        private void move()
        {

            float speed = 15f;

            Vector2 moveVel = (Main.npc[(int)projectile.localAI[0]].Center - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;


            }
            else
            {
                projectile.localAI[0] = -1;
                projectile.ai[1] = projectile.localAI[0];

            }
            projectile.velocity = moveVel;

            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 98, 0, 0, 200, default(Color), 1.5f);
            Main.dust[DustID].noGravity = true;

        }








        private void Target()
        {
            int target = -1;
            int targetMag = 300;
            for (int whichNpc = 0; whichNpc < 200; whichNpc++)
            {
                if (Main.npc[whichNpc].CanBeChasedBy(this, false))
                {

                    float DistanceProjtoNpc = Vector2.Distance(Main.npc[whichNpc].Center, projectile.Center);
                    if (DistanceProjtoNpc < targetMag && whichNpc != projectile.ai[1])
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;

                    }
                }
            }

            projectile.localAI[0] = target;

        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }






    }
}
