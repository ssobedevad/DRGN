using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.ThrowingFlasks
{
    public class ElectricFlask : ThrowingFlask
    {
        private int[,] shootAngles = new int[4, 2] { { 0, 8 }, { 8, 0 }, { 0, -8 }, { -8, 0 }, };
        public override void DeathEffect()
        {           
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("ElectricExplosion"), projectile.damage, projectile.knockBack, projectile.owner);
            for (int i = 0; i < 4; i++)
            {
                Projectile.NewProjectile(projectile.Center, new Vector2(shootAngles[i, 0], shootAngles[i, 1]), mod.ProjectileType("ElectroBallFriendly"), projectile.damage, projectile.knockBack, projectile.owner);
            }
        }
    }
}