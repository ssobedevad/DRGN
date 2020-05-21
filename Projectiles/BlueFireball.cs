using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class BlueFireball : ModProjectile
    {
        
        public override void SetDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = 1;
            projectile.hostile = true;
            projectile.ranged = true;
            projectile.tileCollide = true;
            
            projectile.penetrate = -1;
            

        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
                

            }

            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 99, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 2f);
            Main.dust[DustID].noGravity = true;
            projectile.rotation += 0.1f;
        }
        public override void Kill(int timeleft)
        { Projectile.NewProjectile(projectile.Center + new Vector2 (0,-60), Vector2.Zero, mod.ProjectileType("BlueFireGeyser"), projectile.damage, 0f, projectile.owner); }


    }

}

