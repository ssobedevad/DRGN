using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace DRGN.Projectiles
{
    // The following laser shows a channeled ability, after charging up the laser will be fired
    // Using custom drawing, dust effects, and custom collision checks for tiles
    public class VoidBeamWarning : ModProjectile
    {
        
        //The distance charge particle from the npc center
        private const float START_DISTANCE = 20f;
        // MAx possible laser 
        private const float MAX_LENGTH = 2200f;
        // rotation
        

        // The actual distance is stored in the ai1 field
        // By making a property to handle this it makes our life easier, and the accessibility more readable
        public float laserLength
        {
            get { return projectile.ai[1]; }
            set { projectile.ai[1] = value; }
        }

        


        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = false;
            projectile.friendly = false;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.hide = false;
            projectile.aiStyle = -1;

        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {

            

            SetLaser();


            DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], projectile.Center,
                   projectile.velocity, 10f, projectile.damage, -1.57f, 1f, laserLength, Color.White, (int)START_DISTANCE);

            return false;


        }

        // The core function of drawing a laser
        public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
        {
            float r = unit.ToRotation() + rotation;

            // Draws the laser 'body' -- laserLength / maxDist
            for (float i = transDist; i <= maxDist; i += step)
            {
                Color c = Color.White;
                var origin = start + i * unit;
                spriteBatch.Draw(texture, origin - Main.screenPosition,
                  new Rectangle(0, 26, 6, 26), i < transDist ? Color.Transparent : c, r,
                    new Vector2(6 * .5f, 26 * .5f), scale, 0, 0);

            }

            // Draws the laser 'tail'
            spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
                new Rectangle(0, 0, 6, 26), Color.White, r, new Vector2(6 * .5f, 26 * .5f), scale, 0, 0);

            // Draws the laser 'head'
            spriteBatch.Draw(texture, start + (maxDist + step) * unit - Main.screenPosition,
                new Rectangle(0, 52, 6, 26), Color.White, r, new Vector2(6 * .5f, 26 * .5f), scale, 0, 0);
        }

        // Change the way of collision check of the projectile
        

        // Set custom immunity time on hitting an NPC



        // The AI of the projectile
        public override void AI()
        {

            
           
            CheckKill();
            SpawnDusts();
            CastLights();

        }

        private void SpawnDusts()
        {
            Vector2 unit = projectile.velocity * -1;
            Vector2 dustPos = projectile.Center + projectile.velocity * laserLength;

            for (int i = 0; i < 2; ++i)// end dust
            {
                float num1 = projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
                float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
                Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
                Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, 226, dustVel.X, dustVel.Y)];
                dust.noGravity = true;
                dust.scale = 1.2f;
                dust = Dust.NewDustDirect(projectile.Center, 0, 0, 31,
                    -unit.X * laserLength, -unit.Y * laserLength);
                dust.fadeIn = 0f;
                dust.noGravity = true;
                dust.scale = 0.88f;
                dust.color = Color.Cyan;
            }

            if (Main.rand.NextBool(5))
            {
                Vector2 offset = projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * projectile.width; // start dust
                Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
                dust.velocity *= 0.5f;
                dust.velocity.Y = -Math.Abs(dust.velocity.Y);
                unit = dustPos - projectile.Center;
                unit.Normalize();
                dust = Main.dust[Dust.NewDust(projectile.Center + 55 * unit, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
                dust.velocity = dust.velocity * 0.5f;
                dust.velocity.Y = -Math.Abs(dust.velocity.Y);
            }
        }

        public override void Kill(int time)
        {
            Projectile.NewProjectile(projectile.Center, projectile.velocity, ModContent.ProjectileType<VoidDeathRay>(), projectile.damage, projectile.knockBack);
        }
        /*
         * Sets the end of the laser position based on where it collides with something, and set velocity 
         */
        private void SetLaser()
        {
            Vector2 diff = projectile.velocity;
            diff.Normalize();
            projectile.velocity = diff;

            laserLength = MAX_LENGTH;
        }

        private void CheckKill()
        {
            // Kill the projectile if the npc isnt active or pushes in ai[0] of -1 
            if (projectile.timeLeft > (DRGNModWorld.MentalMode ? 30 : Main.expertMode ? 45 : 60)) { projectile.timeLeft = (DRGNModWorld.MentalMode ? 30 : Main.expertMode ?45 : 60); }

        }



        

        private void CastLights()
        {
            // Cast a light along the line of the laser
            DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
            Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (laserLength - START_DISTANCE), 26, DelegateMethods.CastLight);
        }

        public override bool ShouldUpdatePosition() => false;

        /*
         * Update CutTiles so the laser will cut tiles (like grass)
         */
        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 unit = projectile.velocity;
            Utils.PlotTileLine(projectile.Center, projectile.Center + unit * laserLength, (projectile.width + 16) * projectile.scale, DelegateMethods.CutTiles);
        }
    }
}