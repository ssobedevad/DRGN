using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
//using System.Windows.Shapes;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles
{
    // Template laser projectile, optional charge / collision and kill flag 
    // Using custom drawing, dust effects, and custom collision checks for tiles
    public class SingleLighteningMini : ModProjectile
    {

        // Pass in ai[0]  = projectile .owner 
        //  ai[1] = number of strikes before killing , -1 = infinite! 
        // projectile.center = start point
        // projectile.velocity = direction to strike 

        // boolean for stopping when collide with ground
        // if false then velocity must be desired vector for end point from start 
        // eg this to strike continually , from 1000 above place to random -300,300 difference in x every 500 pixels down 
        // can then update projectile[projid].ai[1] to turn off if needed
        // int projid = Projectile.NewProjectile(Main.player[npc.target].Center + new Vector2(0, -1000f), new Vector2((float)Main.rand.Next(-300, 300), 500f),	mod.ProjectileType("SingleLightening"), (int)1, 1f, 0, (float) npc.whoAmI, -1);

        public bool useCollide = true;
        // The maximum charge value
        private const float MAX_LENGTH = 2200f;

        private List<Line> Segments = new List<Line>();

        private float Alpha { get; set; }
        private float FadeInRate { get; set; }
        private Color Tint { get; set; }

        public bool IsComplete { get { return Alpha >= 1; } }

        private const int Sway = 200;  // how far lightening points can deviate from the center line 
        private const float Jaggedness = .008f;///  = (1 / sway) as a decimal ;
		private const int segmentFactor = 5; // distance between intermediate segments to create  , less = smoother

        public float lightningLength = 2000f;


        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true; 
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            projectile.hide = false;
            projectile.timeLeft = 6000;
            projectile.aiStyle = -1;
            projectile.position = projectile.Center;
            projectile.penetrate = 10;
            Alpha = 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            // only draw if the owning NPC is >= 0   and the bolt segments have been calculated 
            if (projectile.ai[0] >= 0 && Segments.Count > 0)
            {

                //			spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);				
                foreach (var segment in Segments)
                    segment.Draw(spriteBatch, Main.projectileTexture[projectile.type], Tint * (Alpha * 0.6f));
                //				spriteBatch.End();
            }
            return false;
        }

        public virtual void UpdateFade()
        {
            Alpha += FadeInRate;
        }

        // Change the way of collision check of the projectile
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 unit = projectile.velocity;
            float point = 0f;
            // Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
            // It will look for collisions on the given line using AABB
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center,
                projectile.Center + unit * lightningLength
                , 22, ref point);
        }


        // The AI of the projectile
        public override void AI()
        {
            // check if npc has requested kill , or if weve reached requested number of strikes  (ai[1])
            CheckKill();
            if (IsComplete && projectile.ai[1] == 0)
            {
                projectile.active = false;
                Kill(1);
            }
            else
            {
                if (Segments.Count == 0 || IsComplete)
                {
                    if (projectile.ai[1] > 0) projectile.ai[1] = projectile.ai[1] - 1;
                    SetLightningLength();
                    Segments = CreateBolt(projectile.Center, projectile.Center + projectile.velocity * lightningLength, 4);
                    Tint = new Color(0.9f, 0.8f, 1f);
                    Alpha = 0f;
                    FadeInRate = 0.2f;
                }
                else { UpdateFade(); }
            }
            SpawnDusts();

        }

        


        private void SpawnDusts()
        {
            Vector2 unit = projectile.velocity * -1;
            Vector2 dustPos = projectile.Center + projectile.velocity * lightningLength;

            for (int i = 0; i < 2; ++i)
            {
                float num1 = projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
                float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
                Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
                Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, 226, dustVel.X, dustVel.Y)];
                dust.noGravity = true;
                dust.scale = 1.2f;
                dust = Dust.NewDustDirect(projectile.Center, 0, 0, 31,
                    -unit.X * lightningLength, -unit.Y * lightningLength);
                dust.fadeIn = 0f;
                dust.noGravity = true;
                dust.scale = 0.88f;
                dust.color = Color.Cyan;
            }

            if (Main.rand.NextBool(5))
            {
                Vector2 offset = projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * projectile.width;
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

        /*
		 * Sets velocity then the end of the lightening position based on where it collides with something
		 */
        private void SetLightningLength()
        {

            Vector2 dirn = projectile.velocity;  // Main.npc[(int)projectile.ai[0]].velocity - use that to follow npc veloctiy

            dirn.Normalize();
            projectile.velocity = dirn;
            if (!useCollide)
            {
                lightningLength = MAX_LENGTH;
            }
            else
            {
                for (lightningLength = 0; lightningLength <= MAX_LENGTH; lightningLength += 5f)
                {
                    var start = projectile.Center + dirn * lightningLength;
                    if (!Collision.CanHit(projectile.Center, 1, 1, start, 1, 1))
                    {
                        lightningLength -= 5f;
                        break;
                    }
                }
            }
        }

        private void CheckKill()
        {
            // Kill the projectile if NPC is no longer active or ai[0] = -1  is set 
            if (projectile.ai[0] == -1 || Main.npc[(int)projectile.ai[0]].active == false)
            {
                projectile.active = false;

            }
        }



        // dont use normal updating of position 
        public override bool ShouldUpdatePosition() => false;

        /*
		 * Update CutTiles so the laser will cut tiles (like grass)
		 */
        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 unit = projectile.velocity;
            Utils.PlotTileLine(projectile.Center, projectile.Center + unit * lightningLength
                , (projectile.width + 16) * projectile.scale, DelegateMethods.CutTiles);
        }

        protected static List<Line> CreateBolt(Vector2 source, Vector2 dest, int thickness)
        {
            var results = new List<Line>();
            Vector2 tangent = dest - source;
            Vector2 normal = Vector2.Normalize(new Vector2(tangent.Y, -tangent.X));
            float length = tangent.Length();
            int rnd;
            float rndf;
            List<float> positions = new List<float>();
            positions.Add(0);

            for (int i = 0; i < (length / segmentFactor); i++)
            {
                rnd = Main.rand.Next(0, 1000);
                rndf = ((float)rnd) / 1000;
                positions.Add(rndf);
            }
            positions.Sort();

            Vector2 prevPoint = source;
            float prevDisplacement = 0;
            float randomDisp;
            const int randSway = Sway;

            for (int i = 1; i < positions.Count; i++)
            {
                float pos = positions[i];

                // used to prevent sharp angles by ensuring very close positions also have small perpendicular variation.
                // fscale > 0 and < 1 
                float fscale = length * (pos - positions[i - 1]) * Jaggedness;

                // defines an envelope. Points near the middle of the bolt can be further from the central line, as they get near the end they get pulled in .
                float pullin = pos > 0.95f ? 20 * (1 - pos) : 1;

                randomDisp = (float)Main.rand.Next(-randSway, randSway);

                float displacement = randomDisp;

                //if (i < 20 ){
                //	Main.NewText("length " + length  + " displacement " + displacement + "prev " + prevDisplacement + " fscale " + fscale + " env " + envelope + " rand" + randomDisp, 60, 60, 60);
                //	Main.NewText("i " + i + " pos " + pos + " -1 " + positions[i - 1] + " diff " + (Jaggedness * length * (pos - positions [i-1])) + " jag " + Jaggedness, 60, 60, 60 );
                //}
                displacement -= (displacement - prevDisplacement) * (1 - fscale);

                displacement *= pullin;

                //if (i % 10 == 0 && i < 50) { 
                //  Main.NewText(" i " + i + " displacement " + displacement + "prev " + prevDisplacement + " fscale " + ( 1 - fscale) + " env " + envelope + " rand" + randomDisp / 100, 60, 60, 60);
                //}
                Vector2 point = source + (pos * tangent) + (displacement * normal);
                results.Add(new Line(prevPoint, point, thickness, new Color(0.9f, 0.8f, 1f)));
                prevPoint = point;
                prevDisplacement = displacement;
            }
            results.Add(new Line(prevPoint, dest, thickness, new Color(0.9f, 0.8f, 1f)));

            return results;
        }
    }
}
