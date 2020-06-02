using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class BiggerBomb : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 44;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            
            projectile.penetrate = 1;
            
           
            projectile.tileCollide = true;

        }
        public override void AI()
        {
            projectile.velocity.Y += 0.6f;
            if (projectile.velocity.Y >= 16) { projectile.velocity.Y = 16; }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i ++)
            { Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-25, 25), Main.rand.Next(-25, 25), mod.ProjectileType("Bomb"), projectile.damage,projectile.knockBack); }
            Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("MegaFlareExplosion"), projectile.damage/2, 0f, projectile.owner);



        }

        




    }
}


