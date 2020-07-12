using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework;
using System.IO;
using System.Collections.Generic;

namespace DRGN.Projectiles
{
    public class CosmoSpearProj : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 13;
            projectile.width = 13;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            
            projectile.damage = 100;


        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            projectile.ai[0] += 1;
            if ((projectile.ai[0] %=5)== 1) { Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 5, mod.ProjectileType("CelestialSwarm"), projectile.damage, projectile.knockBack,Main.myPlayer);
                
            }
        }


    }
}
