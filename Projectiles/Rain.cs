using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.Projectiles
{
    public class Rain : ModProjectile
    {

        public override void SetDefaults()
        {

            projectile.height = 34;
            projectile.width = 24;
            projectile.aiStyle = 1;
            projectile.scale = 1f;
            projectile.hostile = true;
           

            projectile.penetrate = 1;
            projectile.alpha = 128;


        }

        public override void AI()
        {




            if (Main.rand.Next(2) == 0)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 33, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 2f);
                Main.dust[DustID].noGravity = true;
            }


        }
        
    }
}
