using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Minion
{
    // The following laser shows a channeled ability, after charging up the laser will be fired
    // Using custom drawing, dust effects, and custom collision checks for tiles
    public class FrogTongueMinion : ModProjectile
    {
        private bool retract;
        private int realDist;
        //The distance charge particle from the npc center
        private const float START_DISTANCE = 10f;
        // MAx possible laser 
        private const float MAX_LENGTH = 900;
        // rotation
        //private const float ROTATION_SPEED = 0.015f;

        // The actual distance is stored in the ai1 field
        // By making a property to handle this it makes our life easier, and the accessibility more readable
        public float laserLength
        {
            get { return projectile.ai[1]; }
            set { projectile.ai[1] = value; }
        }

        // Are we at max charge? With c#6 you can simply use => which indicates this is a get only property


        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.hide = false;
            projectile.aiStyle = -1;
            projectile.ai[0] = 0;
            ProjectileID.Sets.MinionShot[projectile.type] = true;
            retract = false;
            realDist = 50;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.ai[0] >= 0)
            {
                if (projectile.velocity.X > 0)
                { projectile.Center = Main.projectile[(int)projectile.ai[0]].Top + new Vector2(10, 4); }
                else
                { projectile.Center = Main.projectile[(int)projectile.ai[0]].Top + new Vector2(-10, 4); }

                SetLaser();


                DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], projectile.Center,
                       projectile.velocity, 3f, projectile.damage, -1.57f, 1f, laserLength, Color.White, (int)START_DISTANCE);
            }
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
                  new Rectangle(0, 16, 14, 12), i < transDist ? Color.Transparent : c, r,
                    new Vector2(14 * .5f, 12 * .5f), scale, 0, 0);

            }

            // Draws the laser 'tail'
            spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
                new Rectangle(0, 0, 14, 12), Color.White, r, new Vector2(14 * .5f, 12 * .5f), scale, 0, 0);

            // Draws the laser 'head'
            spriteBatch.Draw(texture, start + (maxDist + step) * unit - Main.screenPosition,
                new Rectangle(0, 34, 14, 12), Color.White, r, new Vector2(14 * .5f, 12 * .5f), scale, 0, 0);
        }

        // Change the way of collision check of the projectile
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            // We can only collide if we are at max charge, which is when the laser is actually fire    
            
            Vector2 unit = projectile.velocity;
            float point = 0f;
            //Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
            //It will look for collisions on the given line using AABB
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center,
             projectile.Center + unit * laserLength, 10, ref point);

        }

        // Set custom immunity time on hitting an NPC
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
         
            retract = true;
            target.AddBuff(mod.BuffType("Melting"),120);
        }


        // The AI of the projectile
        public override void AI()
        {
            int retractSpeed = 18;
            


            //projectile.velocity = Rotate(projectile.velocity, ROTATION_SPEED);
            CheckKill();
            //SpawnDusts();
            // CastLights();
            
            if (realDist < 900 && retract == false)
            { realDist += retractSpeed * 3; }
            else { realDist -= retractSpeed; retract = true; }
            if (retract == true && realDist <= 10f && projectile.ai[0] > -1) 
            {
                
                projectile.ai[0] = -1;
                

            }
            
        }

        
        public override void Kill(int time)
        {
            
        }
        /*
         * Sets the end of the laser position based on where it collides with something, and set velocity 
         */
        private void SetLaser()
        {
            Vector2 diff = projectile.velocity;
            diff.Normalize();
            projectile.velocity = diff;
            //Vector2 rotatedVelocity = Rotate(diff, projectile.ai[1]);
            laserLength = realDist;
        }

        private void CheckKill()
        {
            // Kill the projectile if the npc isnt active or pushes in ai[0] of -1 
            if (projectile.ai[0] == -1 || Main.npc[(int)projectile.ai[0]].active == false)
            {
                projectile.active = false;
               
            }

        }



        private static Vector2 Rotate(Vector2 v, float radians)
        {
            double ca = Math.Cos(radians);
            double sa = Math.Sin(radians);
            return new Vector2((float)(ca * v.X - sa * v.Y), (float)(sa * v.X + ca * v.Y));
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