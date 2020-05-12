using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.Projectiles
{
    public class DragonFireballProjHostile : ModProjectile
    {
    
        public override void SetDefaults()
        {
            Main.projFrames[projectile.type] = 7;
            projectile.height = 64;
            projectile.width = 64;
            projectile.aiStyle = 0;
            projectile.hostile = true;
            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.alpha = 128;
            

        }

        public override void AI()
        {
            

            if (++projectile.frameCounter >= 7)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
                projectile.alpha = 0;
                
            }
            if (Main.rand.Next(2) == 0)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 35, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 2f); 
                Main.dust[DustID].noGravity = true; 
            }
        

        }

    }
}
