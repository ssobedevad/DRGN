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
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            projectile.scale = 2f;
            projectile.damage = 100;


        }
        public override void AI()
        {
        if (Main.rand.Next(0,6)==1) { Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("CelestialSwarm"), projectile.damage, projectile.knockBack,Main.myPlayer);
                
            }
        }


    }
}
