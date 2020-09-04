using DRGN.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles
{
    public class ShadeThrowingKnife : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.height = 8;
            projectile.width = 8;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.tileCollide = true;
            projectile.penetrate = 2;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);

        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            projectile.ai[1]++;
            if (projectile.ai[1] > 40) { projectile.velocity.Y += 0.2f; }
            if (projectile.ai[0] == 0 && projectile.timeLeft > 35) { projectile.timeLeft = 35; }
        }
        public override void Kill(int timeleft)
        {
            if (projectile.ai[0] == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 vel = DavesUtils.Rotate(projectile.velocity * 1.4f, 0.5f * (i == 0 ? 1f : -1f));
                    Projectile.NewProjectile(projectile.Center, vel, projectile.type, (int)(projectile.damage * 1.25f), projectile.knockBack, projectile.owner, 1);
                }
                Projectile.NewProjectile(projectile.Center, projectile.velocity * 1.4f, projectile.type, (int)(projectile.damage * 1.25f), projectile.knockBack, projectile.owner, 1);
            }
        }
    }
}

