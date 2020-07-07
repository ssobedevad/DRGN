using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.Projectiles
{
    public class VoidBladeProj : ModProjectile
    {

        private Player player;



        public override void SetDefaults()
        {
            projectile.width = 128;
            projectile.height = 128;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            
          
        }
        public override void AI()
        {
            player = Main.player[projectile.owner];
            projectile.Center = player.Center;
            projectile.rotation += 0.3f;
            
            if (!player.channel) { projectile.active = false; }
        }

        
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D text = ModContent.GetTexture(Texture);
            Vector2 vect2 = new Vector2(projectile.Center.X - Main.screenPosition.X, projectile.position.Y + projectile.height / 2 - Main.screenPosition.Y);
            Rectangle rect2 = new Rectangle(0, 0, text.Width, text.Height);
            spriteBatch.Draw(
                   text,
                     vect2, rect2, lightColor, projectile.rotation, new Vector2(text.Width / 2, text.Height / 2), 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}