using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.Projectiles
{
    public class VoidScytheProj : ModProjectile
    {
        private float speed;
        
        
        public override void SetDefaults()
        {

            projectile.height = 80;
            projectile.width = 80;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 12;
            
            projectile.ai[0] = 0;
            projectile.tileCollide = false;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);


        }
        public override void AI()
        {
          
            if (++projectile.frameCounter >= 12)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];


            }

            projectile.rotation += 0.5f;



            if (Main.rand.Next(0, 2) == 1)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 98, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 0, default(Color), 2f);
                Main.dust[DustID].noGravity = true;


            }
            if (projectile.ai[0] >= 25) { move(); }
            projectile.ai[0] += 1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {


            target.AddBuff(mod.BuffType("VoidBuff"), 600);

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
