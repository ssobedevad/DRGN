using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.Projectiles
{
    public class RainMini : ModProjectile
    {

        public override void SetDefaults()
        {

            projectile.height = 17;
            projectile.width = 12;
            projectile.aiStyle = 1;
            projectile.scale = 1f;
            projectile.friendly = true;
           

            projectile.penetrate = 1;
            projectile.alpha = 128;
            ProjectileID.Sets.MinionShot[projectile.type] = true;

        }

        public override void AI()
        {




            if (Main.rand.Next(2) == 0)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 33, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 1f);
                Main.dust[DustID].noGravity = true;
            }


        }
        
    }
}
