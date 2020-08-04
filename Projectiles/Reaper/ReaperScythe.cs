using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Remoting.Messaging;

namespace DRGN.Projectiles.Reaper
{
    public class ReaperScythe : ModProjectile
    {

        private float RetractSpeed;
        public Texture2D projectileTexture;
        public override void SetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.ai[0] = 0;

            projectile.tileCollide = false;
            projectile.penetrate = -1;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);


        }
        private void Init()
        {
            RetractSpeed = projectile.velocity.Length();
            projectile.width = projectileTexture.Width;
            projectile.height = projectileTexture.Height;
        }

        public override void AI()
        {
          
           if(projectile.localAI[0] ==0)
            { Init(); projectile.localAI[0] = 1; }
            if (projectile.velocity.Length() > RetractSpeed * 2f) { projectile.velocity = Vector2.Normalize(projectile.velocity) * RetractSpeed * 2f; }

            projectile.rotation += 0.3f;
            if(projectile.ai[0] > 0 && projectile.ai[0]  < 20)
            { projectile.velocity *= 0.9f; }
            if (projectile.ai[0] >= 0)
            {
                projectile.ai[0] -= 1;
               
            }
            
            else
            {
                move();




            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if(Main.rand.Next(1,100) < projectile.ai[1])
            { crit = true; }
        }
        private void move()
        {

            
            Vector2 moveTo = Main.player[projectile.owner].Center;
            Vector2 moveVel = (moveTo - projectile.Center);
            float magnitude = Magnitude(moveVel);
            if (Vector2.Distance(projectile.Center,moveTo) > 40)
            {
                moveVel *= RetractSpeed / magnitude;
                projectile.timeLeft = 2;
                projectile.velocity = projectile.velocity + (moveVel/15f) ;
            }
            else { projectile.alpha += 50; }

        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {

           

            spriteBatch.Draw(
                 projectileTexture,
                   projectile.Center - Main.screenPosition, null, lightColor, projectile.rotation,  new Vector2(projectile.width/2, projectile.height/2), 1f, SpriteEffects.None, 0f);
           
        }

        private float Magnitude(Vector2 mag)// does funky pythagoras to find distance between two points
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }


    }
}
