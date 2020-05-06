using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

namespace DRGN.Projectiles
{
    public class VoidScytheProj : ModProjectile
    {
        private float speed;
        private Vector2 moveTo;
        private Vector2 moveVel;
        public override void SetDefaults()
        {

            projectile.height = 30;
            projectile.width = 30;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 12;
            projectile.scale = 2.4f;
            projectile.ai[0] = 0;
            projectile.tileCollide = false;
            


        }
        public override void AI()
        {
            if (++projectile.frameCounter >= 12)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
                

            }
            
            projectile.rotation += 0.5f;
         
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("VoidScytheProjShadow"), 0, 0, Main.myPlayer, projectile.frame, projectile.rotation);
            
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
