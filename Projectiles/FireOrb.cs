using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles
{
    public class FireOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            FlailsAI.projectilesToDrawShadowTrails.Add(projectile.type);
        }
        public override void AI()
        {
            projectile.rotation += 0.3f;
            if (projectile.ai[0] == 0 && projectile.timeLeft > 35) { projectile.timeLeft = 35; }
            int dustid = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire);
            Main.dust[dustid].noGravity = true;
        }

        public override void Kill(int timeleft)
        {
            int numBlasts = Main.rand.Next(3, 8);
            float step = 360 / numBlasts;
            for (int i = 0; i < numBlasts; i++)
            {
                Vector2 vel = DavesUtils.Rotate(new Vector2(0, -8), MathHelper.ToRadians(step * i));
                Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("FireOrbSplit"), (int)(projectile.damage * 0.8f), projectile.knockBack * 0.8f, projectile.owner) ;
            }

        }
    }
}