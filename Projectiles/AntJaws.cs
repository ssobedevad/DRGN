using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class AntJaws : ModProjectile
    {

        public override void SetDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            projectile.height = 32;
            projectile.width = 64;
            projectile.aiStyle = 0;
            projectile.hostile = true;
            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.light = 0.1f;

        }

        public override void AI()
        {
            projectile.spriteDirection = -projectile.direction;
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
                

            }



        }
    }

}