using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class TrueUnstableMeteorShatter : ModProjectile
    {
        
        private int homeCounter;
        public override void SetDefaults()
        {
           
            projectile.width = 16;
            projectile.height = 16;
            projectile.scale = 1f;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            homeCounter = 0;
            projectile.light = 3f;
            
            projectile.alpha = 80;


        }
        public override void AI()
        {

            if (homeCounter >= 40) { move(); }
            else { projectile.velocity.Y += 0.01f; }
            homeCounter += 1;

        }     

        private void move()
        {

            float speed = 15f;
            Vector2 moveTo = new Vector2((int)projectile.ai[0],(int)projectile.ai[1]);
            if (Main.rand.Next(0, 5) == 1)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 74, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 1.8f);
                Main.dust[DustID].noGravity = true;
            }
            Vector2 moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;


            }
            else if (projectile.timeLeft >=2) { projectile.timeLeft = 2; }
            projectile.velocity = moveVel;

        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override void Kill(int timeleft)
        { Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("ArcanumExplosion"), projectile.damage, 0f, projectile.owner); }


    }
}