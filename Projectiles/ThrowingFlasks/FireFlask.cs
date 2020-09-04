using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.ThrowingFlasks
{
    public class FireFlask : ThrowingFlask
    {       
        public override void DeathEffect()
        {
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("FireExplosion"), projectile.damage, projectile.knockBack, projectile.owner);            
        }
    }
}