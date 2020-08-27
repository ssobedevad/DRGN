using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace DRGN.Projectiles.GalacticGuardian
{
    // The following laser shows a channeled ability, after charging up the laser will be fired
    // Using custom drawing, dust effects, and custom collision checks for tiles
    public class GalacticBeamWarning : ModProjectile
    {

        //The distance charge particle from the npc center
        private const float START_DISTANCE = 20f;
        // MAx possible laser 
        private const float MAX_LENGTH = 4000f;
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
            projectile.alpha += 2;
            if(projectile.alpha > 255) { projectile.alpha = 255; }


            CheckKill();
           
        }

       
        public override void Kill(int time)
        {
            Projectile.NewProjectile(projectile.Center, projectile.velocity, mod.ProjectileType("GalacticBeam"), projectile.damage, projectile.knockBack , 255 , 0 , -1);
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
            if (projectile.timeLeft > (DRGNModWorld.MentalMode ? 30 : Main.expertMode ? 45 : 60)) { projectile.timeLeft = (DRGNModWorld.MentalMode ? 30 : Main.expertMode ? 45 : 60); }

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