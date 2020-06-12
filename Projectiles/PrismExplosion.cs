﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class PrismExplosion : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 256;
            projectile.width = 176;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 5;
            projectile.ai[1] = 0;

            projectile.damage = 100;
            projectile.alpha = 160;
        }
        public override void AI()
        {
            projectile.ai[1] += 1;
            if (projectile.ai[1] % 5 == 0)
            {

                if (projectile.frame == 4) { projectile.active = false; }
                else
                {
                    projectile.alpha -= 30;
                    projectile.frame += 1;
                }

            }

        }





    }
}


