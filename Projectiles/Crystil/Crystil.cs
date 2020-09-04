using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles.Crystil
{
    public class Crystil : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 30;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
        }
        public override void Kill(int timeleft)
        {
            DavesUtils.CrystilExplosion(projectile.velocity, projectile.damage, projectile.knockBack, projectile.owner, projectile.Center, mod.ProjectileType("CrystilShard"), Main.player[projectile.owner]);
        }
    }
}
