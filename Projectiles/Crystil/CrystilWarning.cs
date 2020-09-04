using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Crystil
{
    public class CrystilWarning : ModProjectile
    {
        private const float START_DISTANCE = 20f;
        private const float MAX_LENGTH = 3000f;
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
            DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], projectile.Center, projectile.velocity, 10f, -1.57f, laserLength, (int)START_DISTANCE);
            return false;
        }
        public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation = 0f, float maxDist = 2000f, int transDist = 50)
        {
            Color c = Color.White;
            c.R = (byte)(c.R * 20 / 30);
            c.G = (byte)(c.G * 20 / 30);
            c.B = (byte)(c.B * 20 / 30);
            c.A = (byte)(c.A * 20 / 30);
            float r = unit.ToRotation() + rotation;
            for (float i = transDist; i <= maxDist; i += step)
            {
                
                var origin = start + i * unit;
                spriteBatch.Draw(texture, origin - Main.screenPosition, new Rectangle(0,12,10,14), i < transDist ? Color.Transparent : c, r, new Vector2(5, 7), 1f, 0, 0);
            }
            var EndPos = start + (maxDist * unit) + (10f * unit);
            spriteBatch.Draw(texture, start + (20 * unit) - Main.screenPosition, new Rectangle(0, 0, 10, 10), c, r, new Vector2(5,5), 1f, 0, 0);
            spriteBatch.Draw(texture, EndPos - Main.screenPosition, new Rectangle(0, 28, 10, 10), c, r, new Vector2(5, 5), 1f, 0, 0);
        }
        public override void AI()
        {
            int timeLeft = DRGNModWorld.MentalMode ? 30 : Main.expertMode ? 45 : 60;
            if (projectile.timeLeft > timeLeft) { projectile.timeLeft = timeLeft; }
        }
        public override void Kill(int time)
        {
            float speed = DRGNModWorld.MentalMode ? 18f : Main.expertMode ? 16f : 14f;
            Projectile.NewProjectile(projectile.Center, Vector2.Normalize(projectile.velocity) * speed, mod.ProjectileType("GiantCrystil"), projectile.damage, projectile.knockBack);
        }
        private void SetLaser()
        {
            if (projectile.ai[0] > -1)
            {
                projectile.Center = Main.npc[(int)projectile.ai[0]].Center;
            }
            Vector2 diff = projectile.velocity;
            diff.Normalize();
            projectile.velocity = diff;
            laserLength = MAX_LENGTH;
        } 
        public override bool ShouldUpdatePosition() => false;
        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 unit = projectile.velocity;
            Utils.PlotTileLine(projectile.Center, projectile.Center + unit * laserLength, (projectile.width + 16) * projectile.scale, DelegateMethods.CutTiles);
        }
    }
}