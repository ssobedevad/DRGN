using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Graphics;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles.Reaper
{
    public class DeathMark : ModProjectile
    {
        
        public override void SetDefaults()
        {                    
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.friendly = true;            
        }
        public override void AI()
        {
            projectile.damage = 0;
            projectile.alpha += 5;
            if (projectile.alpha >= 220) { projectile.Kill(); }
            if (projectile.alpha > 100)
            {
                projectile.velocity.Y -= 0.05f;
            }
        }
    }
}
