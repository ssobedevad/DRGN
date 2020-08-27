using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace DRGN.Projectiles.GalacticGuardian
{
    // The following laser shows a channeled ability, after charging up the laser will be fired
    // Using custom drawing, dust effects, and custom collision checks for tiles
    public class GalacticBeam : ModProjectile
    {

        //The distance charge particle from the npc center
        private const float START_DISTANCE = 20f;
        // MAx possible laser 
        private const float MAX_LENGTH = 4000f;
        // rotation
        private float laserPercent = 0f;

        // The actual distance is stored in the ai1 field
        // By making a property to handle this it makes our life easier, and the accessibility more readable
        public float laserLength;
        

        // Are we at max charge? With c#6 you can simply use => which indicates this is a get only property


        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.aiStyle = -1;

        }
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindNPCs.Add(index);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if(projectile.ai[1] > -1)
            { projectile.Center = Main.npc[(int)projectile.ai[0]].Center; }


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
                float frameType;
                if(Main.time%10 > 5) { frameType = 0; }
                else { frameType = 30; }
                Color c = Color.White;
                var origin = start + i * unit;
                spriteBatch.Draw(texture, origin - Main.screenPosition,
                new Rectangle(0, (int)frameType, 44, 30), i < transDist ? Color.Transparent : c, r,
                new Vector2(44 * .5f, 30 * .5f), scale, 0, 0);

            }
            
            
           
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
             projectile.Center + unit * laserLength, 22, ref point);

        }

        // Set custom immunity time on hitting an NPC



        // The AI of the projectile
        public override void AI()
        {
            if (!NPC.AnyNPCs(mod.NPCType("GalacticBarrier"))) { projectile.active = false; }
            if (projectile.ai[0] > 0)
            {
                if (Main.npc[(int)projectile.ai[0]].type != mod.NPCType("GalacticBarrier"))
                {
                    if (Main.npc[(int)projectile.ai[0]].type == mod.NPCType("GalacticGuardianDockingStation"))
                    {
                        NPC guardianNPC = Main.npc[(int)Main.npc[(int)projectile.ai[0]].ai[0]];
                        if (guardianNPC.life < guardianNPC.lifeMax / 2)
                        { projectile.active = false; }
                        else if (!(guardianNPC.ai[0] >= 5 && guardianNPC.ai[0] < 13))
                        { projectile.active = false; }
                    }
                    else { projectile.active = false; }
                }
            }
            CheckKill();
           

        }

       

        public override void Kill(int time)
        {

        }
        /*
         * Sets the end of the laser position based on where it collides with something, and set velocity 
         */
        private void SetLaser()
        {
            if (projectile.ai[1] == -1)
            {
                Vector2 diff = projectile.velocity;
                diff.Normalize();
                projectile.velocity = diff;

                laserLength = MAX_LENGTH;
            }
            else
            {
                Vector2 diff = Main.npc[(int)projectile.ai[1]].Center - Main.npc[(int)projectile.ai[0]].Center;
                diff.Normalize();
                projectile.velocity = diff;
                laserPercent += 0.01f;
                if(laserPercent > 1f) { laserPercent = 1f; }
                laserLength = Vector2.Distance(Main.npc[(int)projectile.ai[1]].Center , Main.npc[(int)projectile.ai[0]].Center) * laserPercent;              
            }
        }

        private void CheckKill()
        {
            // Kill the projectile if the npc isnt active or pushes in ai[0] of -1 
            if (projectile.ai[1] < 0)
            {
                if (projectile.timeLeft > 100) { projectile.timeLeft = 100; }
            }
            else { projectile.timeLeft = 100; }

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