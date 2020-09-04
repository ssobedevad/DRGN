using DRGN.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Crystil
{
    public class CrystilKnife : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.height = 8;
            projectile.width = 8;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            projectile.ai[0]++;
            if (projectile.ai[0] > 90) { projectile.velocity.Y += 0.25f; }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            DavesUtils.CrystilExplosion(projectile.velocity, projectile.damage, projectile.knockBack, projectile.owner, projectile.Center, mod.ProjectileType("CrystilShard"), Main.player[projectile.owner]);
        }
    }
}

