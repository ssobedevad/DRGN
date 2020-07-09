using System;
using Terraria;
using Terraria.ModLoader;
namespace DRGN.Projectiles
{
    public class GlitchBullet : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.width = 7;
            projectile.height = 7;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;

        }
        public override void AI()
        { projectile.rotation = projectile.velocity.ToRotation() + 1.57f; }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Bugged"), 40);

        }


    }
}
