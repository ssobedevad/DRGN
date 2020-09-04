using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles
{
    public class AntShuriken : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 1;
        }
        public override void AI()
        {
            projectile.rotation += 1f;
            projectile.velocity.Y += 0.1f;
        }
        public override void Kill(int timeleft)
        {
            for (int i = 0; i < 2; i++)
            { Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("AntBiterJaws"), projectile.damage, projectile.knockBack, projectile.owner); }
        }
    }
}