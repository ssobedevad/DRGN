using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.ThrowingFlasks
{
    public class LunarFlask : ThrowingFlask
    {
        public override void DeathEffect()
        {
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("LunarExplosion"), projectile.damage, projectile.knockBack, projectile.owner);
        }
    }
}