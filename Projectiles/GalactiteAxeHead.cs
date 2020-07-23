using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.Projectiles
{
    public class GalactiteAxeHead : ModProjectile
    {
        private float speed;
        
        public override void SetDefaults()
        {

            projectile.height = 72;
            projectile.width = 72;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);
            
            projectile.ai[0] = 0;
            projectile.tileCollide = false;



        }
        public override void AI()
        {
            
           

            projectile.rotation += 1f;

            

            
            if (projectile.ai[0] >= 25) { move(); }
            projectile.ai[0] += 1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {


            target.AddBuff(mod.BuffType("GalacticCurse"), 120);
            Projectile.NewProjectile(target.Center + new Vector2(Main.rand.Next(-5, 5), -1000), new Vector2(0, Main.rand.Next(1, 5)), mod.ProjectileType("OmegaBeeStar"), projectile.damage, 0f, projectile.owner, projectile.Center.Y - 10);
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("GalacticExplosion"), projectile.damage, 0f, projectile.owner);

        }
        private void move()
        {

            speed = 40f;
            Vector2 moveTo = Main.player[projectile.owner].Center;
            Vector2 moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (magnitude > speed)
            {
                moveVel *= speed / magnitude;
                projectile.timeLeft = 2;
                projectile.velocity = moveVel;
            }
            else { projectile.alpha = 200; }

        }
        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        


    }
}
