using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace DRGN.Projectiles
{
    public class VoidBeamWarning : ModProjectile
    {       
        private const float START_DISTANCE = 20f;
        private const float MAX_LENGTH = 2200f;       
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
            DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], projectile.Center,projectile.velocity, 10f, -1.57f, 1f, laserLength, (int)START_DISTANCE);
            return false;
        }
        public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation = 0f, float scale = 1f, float maxDist = 2000f, int transDist = 50)
        {
            float r = unit.ToRotation() + rotation;
            for (float i = transDist; i <= maxDist; i += step)
            {
                Color c = Color.White;
                var origin = start + i * unit;
                spriteBatch.Draw(texture, origin - Main.screenPosition,null, i < transDist ? Color.Transparent : c, r,texture.Size()/2, scale, 0, 0);
            }          
        }
        public override void AI()
        {                      
            CheckKill();
            CastLights();
        }
        public override void Kill(int time)
        {
            Projectile.NewProjectile(projectile.Center, projectile.velocity, ModContent.ProjectileType<VoidDeathRay>(), projectile.damage, projectile.knockBack);
        }
        private void SetLaser()
        {
            Vector2 diff = projectile.velocity;
            diff.Normalize();
            projectile.velocity = diff;
            laserLength = MAX_LENGTH;
        }
        private void CheckKill()
        {
            int timeLeft = DRGNModWorld.MentalMode ? 30 : Main.expertMode ? 45 : 60;
            if (projectile.timeLeft > timeLeft) { projectile.timeLeft = timeLeft; }
        }      
        private void CastLights()
        {           
            DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
            Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (laserLength - START_DISTANCE), 26, DelegateMethods.CastLight);
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