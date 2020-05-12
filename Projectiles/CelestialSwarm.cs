using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class CelestialSwarm : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 4;
            projectile.ai[1] = 0;
            projectile.tileCollide = true;
            projectile.damage = 100;
            projectile.alpha = 120;
        }
        public override void AI()
        {

            if (projectile.ai[1] == 0)
            {

                int sprite = ((Main.rand.Next(0, 4)));
                projectile.frame = sprite;
                projectile.ai[1] += 1;
            }
            if (Main.rand.Next(0, 12) == 1)
            {
                int DustID = Dust.NewDust(projectile.Center, 0, 0, Main.rand.Next(227, 230), 0.0f, 0.0f, 10, default(Color), 1.8f);
            }
        }
        



    }
}


