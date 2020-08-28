using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles
{
    public class ShadowBall : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
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
            int dustid = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("ShadowDust"));
            Main.dust[dustid].noGravity = true;
        }

        public override void Kill(int timeleft)
        {
            for (int i = 0; i < 4; i++)
            {
                int proj = Projectile.NewProjectile(projectile.Center, Vector2.Normalize(Main.MouseWorld - projectile.Center) * 10, mod.ProjectileType("ShadowScream"), (int)(projectile.damage * 0.8f), projectile.knockBack * 0.8f, projectile.owner);
                Main.projectile[proj].localAI[0] = 0;
                Main.projectile[proj].localAI[1] = 5;
            }

        }
    }
}