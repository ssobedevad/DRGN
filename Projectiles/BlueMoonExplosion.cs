using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class BlueMoonExplosion : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 90;
            projectile.width = 56;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 4;
            projectile.ai[1] = 0;
           
            projectile.damage = 100;
            projectile.alpha = 120;
        }
        public override void AI()
        {
            projectile.ai[1] += 1;
            if (projectile.ai[1] % 5 == 0)
            {

                if (projectile.frame == 3) { projectile.active = false; }
                else
                {
                    projectile.alpha -= 30;
                    projectile.frame += 1;
                }
               
            }
           
        }





    }
}


