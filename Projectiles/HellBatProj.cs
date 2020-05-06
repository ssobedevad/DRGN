using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class HellBatProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Main.projFrames[projectile.type] = 5;
            projectile.height =26;
            projectile.width = 24;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            


        }
        
        public override void AI()
        {
            if (++projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
            }
            projectile.spriteDirection = projectile.direction;

        }

    }
}
