using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class GalactiteStarLaser : ModProjectile
    {
        public Vector2 moveTo;
        public override void SetDefaults()
        {

            projectile.height = 34;
            projectile.width = 34;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Main.npc[(int)projectile.ai[0]].CanBeChasedBy(this,false))
            {
                moveTo = Main.npc[(int)projectile.ai[0]].Center;
                Move();
            }
           
            
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        private void Move()
        {
            // Sets the max speed of the npc.


             
            Vector2 move = moveTo - projectile.Center;
            float magnitude = Magnitude(move);
            if (magnitude > 15)
            {
                move *= 15 / magnitude;
            }
            else if (projectile.timeLeft>2)
            { projectile.timeLeft = 2; }

            projectile.velocity = move;


        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override void Kill(int timeleft)
        { Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("GalacticExplosion"), projectile.damage, 0f, projectile.owner); }
    }

}

