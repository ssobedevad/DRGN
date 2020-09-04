using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.ThrowingFlasks
{
    public class MagmaticFlask : ThrowingFlask
    {
        public override void DeathEffect()
        {
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("MegaFlareExplosion"), projectile.damage, projectile.knockBack, projectile.owner);
        }
    }
}