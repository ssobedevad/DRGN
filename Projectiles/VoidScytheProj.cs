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
        
        private Vector2[] oldPos = new Vector2[9] { Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, };
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



        }
        public override void AI()
        {
            for (int i = 8; i > -1; i--)
            {
                if (i == 0) { oldPos[i] = projectile.Center; }
                else
                {
                    oldPos[i] = oldPos[i - 1];

                }



                if (oldPos[i] == Vector2.Zero) { oldPos[i] = projectile.Center; }

            }
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
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            for (int i = 8; i >= 0; i--)
            {
                Vector2 oldV = oldPos[i];
                Vector2 vect = new Vector2(oldV.X - Main.screenPosition.X, oldV.Y - Main.screenPosition.Y);
                Rectangle rect = new Rectangle(0, projectile.frame * 80, projectile.width, projectile.height);

                Color alpha9 = projectile.GetAlpha(Color.White);
                alpha9.R = (byte)(alpha9.R * (10 - (2 * i)) / 20);
                alpha9.G = (byte)(alpha9.G * (10 - (2 * i)) / 20);
                alpha9.B = (byte)(alpha9.B * (10 - (2 * i)) / 20);
                alpha9.A = (byte)(alpha9.A * (10 - (2 * i)) / 20);
                spriteBatch.Draw(
                    ModContent.GetTexture(Texture),
                     vect, rect, alpha9, projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), 1f, SpriteEffects.None, 0f);




            }
            Vector2 vect2 = new Vector2(projectile.position.X + projectile.width / 2 - Main.screenPosition.X, projectile.position.Y + projectile.height / 2 - Main.screenPosition.Y);
            Rectangle rect2 = new Rectangle(0, projectile.frame * 80, projectile.width, projectile.height);
            spriteBatch.Draw(
                    ModContent.GetTexture(Texture),
                     vect2, rect2, Color.White, projectile.rotation, new Vector2(projectile.width / 2, projectile.height / 2), 1f, SpriteEffects.None, 0f);
            return false;

        }
    }
}
