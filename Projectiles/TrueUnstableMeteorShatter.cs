using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class TrueUnstableMeteorShatter : ModProjectile
    {

        public override void SetDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            projectile.width = 16;
            projectile.height = 16;
            projectile.scale = 0.5f;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = -1;
            
            projectile.ai[0] = 0;
            projectile.light = 3f;



        }
        public override void AI()
        {
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
            }
        }

    }
}