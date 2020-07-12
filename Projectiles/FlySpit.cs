using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class FlySpit : ModProjectile
    {
       
        public override void SetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            
           
            projectile.tileCollide = true;

        }
        public override void AI()
        {
            projectile.velocity.Y += 0.2f;
            
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 4; i++)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.Bee, projectile.damage, projectile.knockBack,projectile.owner);
            }
           
        }




    }
}


