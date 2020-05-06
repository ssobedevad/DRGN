using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

namespace DRGN.Projectiles
{
    public class VoidScytheProjShadow : ModProjectile
    {
        
        public override void SetDefaults()
        {

            projectile.height = 13;
            projectile.width = 13;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 12;
            projectile.scale = 2.4f;
            
            projectile.tileCollide = false;



        }
        public override void AI()
        {

            projectile.frame = (int)projectile.ai[0];
            projectile.rotation = projectile.ai[1];
            projectile.alpha += 10;
            if(projectile.alpha >= 255) { projectile.active = false; }

        }


    }
}
