using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.ThrowingFlasks
{
    public class AntFlask : ThrowingFlask
    {
        public override void DeathEffect()
        {
            for (int i = 0; i < 3; i++)
            { Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("AntBiterJaws"), projectile.damage, projectile.knockBack, projectile.owner); }
        }
    }
}