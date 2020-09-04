using DRGN.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles
{
    public class FlareCrystalShard : ModProjectile
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
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            projectile.ai[0]++;
            if (projectile.ai[0] > 40) { projectile.velocity.Y += 0.25f; }
        }
    }
}

