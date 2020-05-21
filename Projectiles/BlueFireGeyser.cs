using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class BlueFireGeyser : ModProjectile
    {
        
        public override void SetDefaults()
        {

            projectile.height = 160;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 5;
            projectile.ai[1] = 0;
            projectile.penetrate = -1;
            
            projectile.damage = 100;
            
        }
        public override void AI()
        {
            projectile.ai[1] += 1;
            if (projectile.ai[1] % 10 == 0)
            {

                if (projectile.frame == 4) { projectile.active = false; Projectile.NewProjectile(projectile.Center, new Vector2(0, -8), mod.ProjectileType("BlueFireGeyserBall"), projectile.damage/2, 0f); }

                
                projectile.frame += 1;
                

            }

        }




    }
}


