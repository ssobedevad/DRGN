using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

namespace DRGN.Projectiles
{
    public class GalactiteAxeHeadShadow : ModProjectile
    {

        public override void SetDefaults()
        {

            projectile.height = 72;
            projectile.width = 72;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;

            projectile.alpha = 100;

            projectile.tileCollide = false;



        }
        public override void AI()
        {

            
            projectile.rotation = projectile.ai[0];
            projectile.alpha += 10;
            if (projectile.alpha >= 255) { projectile.active = false; }

        }


    }
}
