using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class EarthenArrow : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;

        }
        
        public override void Kill(int timeleft)
        { Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("EarthenExplosion"), projectile.damage, 0f, projectile.owner); }


    }
}
