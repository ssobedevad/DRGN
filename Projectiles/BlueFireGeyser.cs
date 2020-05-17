using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class BlueFireGeyser : ModProjectile
    {
        private int cD;
        public override void SetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 4;
            projectile.ai[1] = 0;
            cD = 60;
            projectile.damage = 100;
            
        }
        public override void AI()
        {
            projectile.ai[1] += 1;
            if (cD == 0) { projectile.active = false; }
            if (projectile.ai[1] % 20 == 0)
            {
                if (projectile.frame == 1)
                {
                    projectile.height = 32;
                    projectile.width = 32;
                }
                else if (projectile.frame == 2) 
                {
                    projectile.height = 64;
                    projectile.width = 32;
                }
                else if (projectile.frame == 3) {
                    projectile.height = 128;
                    projectile.width = 32; 
                     }
                else if (projectile.frame == 4)
                {
                    projectile.height = 128;
                    projectile.width = 32;
                    cD -= 1;
                }

                else
                {   
                    
                    
                    projectile.frame += 1;
                }

            }

        }




    }
}


