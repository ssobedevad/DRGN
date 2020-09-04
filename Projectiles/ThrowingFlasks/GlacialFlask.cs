using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.ThrowingFlasks
{
    public class GlacialFlask : ThrowingFlask
    {
        public override void DeathEffect()
        {
            for (int i = 0; i < 10; i++)
            { Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-25, 25), Main.rand.Next(-25, 25), mod.ProjectileType("IceShatter"), projectile.damage, projectile.knockBack, projectile.owner); }
        }
    }
}