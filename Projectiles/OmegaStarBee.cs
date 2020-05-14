using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class OmegaStarBee : ModProjectile
    {
        private int frameCount;
        private int target;
        private bool loose;
        public int whichNpc;
        private Vector2 moveVel;
        private int targetMag;
        private float speed;
        private Vector2 moveTo;
        public override void SetDefaults()
        {

            projectile.height = 20;
            projectile.width = 20;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.light = 2f;
            Main.projFrames[projectile.type] = 4;

            loose = true;
            projectile.damage = 100;

        }
        public override void AI()
        {
            Target();
            if (target == -1) { if (!loose) projectile.velocity = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)); projectile.timeLeft = 10; loose = true; }
            else { move(); }
            if ((frameCount % 5) == 0)

            {

                projectile.frame += 1;
            }
            if (projectile.frame == Main.projFrames[projectile.type])
            { projectile.frame = 0; }
            if (projectile.ai[0] >= 0)
            { projectile.ai[0] -= 1; }
            if (projectile.ai[0] == 0)
            { loose = false; }



        }
        private void move()
        {
            if (projectile.ai[0] == -1)
            {
                loose = false;
            }
            projectile.timeLeft = 10;
            speed = 8f;
            moveTo = Main.npc[target].Center;
            moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;

                projectile.velocity = moveVel;
            }

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
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
                    if (DistanceProjtoNpc < targetMag)
                    {
                        targetMag = (int)DistanceProjtoNpc;
                        target = whichNpc;

                    }
                }
            }


        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        public override void Kill(int timeleft)
        {
            Projectile.NewProjectile(projectile.Center + new Vector2(Main.rand.Next(-5, 5), -1000), new Vector2(0, Main.rand.Next(1, 5)), mod.ProjectileType("OmegaBeeStar"), projectile.damage, 0f, projectile.owner, projectile.Center.Y - 10);
            for (int i = 0; i < 3; i++)
            {
                
                Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-5, 5), projectile.Center.Y + Main.rand.Next(-5, 5), 0, 0, mod.ProjectileType("StarBee"), 80, 1f, projectile.owner, 0);
            }

        }



    }
}


