using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Crystil
{
    public class CrystilShuriken : ModProjectile
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
            DavesUtils.CrystilExplosion(projectile.velocity, projectile.damage, projectile.knockBack, projectile.owner, projectile.Center, mod.ProjectileType("CrystilShard"), Main.player[projectile.owner]);
        }
    }
}