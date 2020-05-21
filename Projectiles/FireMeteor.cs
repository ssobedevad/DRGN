using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class FireMeteor : ModProjectile
    {
        
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 42;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.ai[0] = 0;
            projectile.light = 3f;
            
            
            
        }
        public override void AI()
        {
        if (projectile.ai[0] >= 15)
        { projectile.tileCollide = true; }
            projectile.ai[0] += 1;
            if (Main.rand.Next(2) == 0)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 35, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 1.5f);
                Main.dust[DustID].noGravity = true;
            }
        }
        
    }
}