using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.Projectiles
{
    public class PlasmaKnife : ModProjectile
    {

        public override void SetDefaults()
        {
            
            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = 1;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.magic = true;
           
            projectile.penetrate = 1;
           


        }
        public override void AI()
        {
            if (Main.rand.Next(0, 2) == 1)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 70, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 200, default(Color), 1f);
                Main.dust[DustID].noGravity = true;
            }
        }


    }
}
