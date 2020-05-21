using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class ElectroBall : ModProjectile
    {
        
        public override void SetDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = 0;
            projectile.hostile = true;
            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 220;
            projectile.penetrate = -1;
            

        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
                

            }
            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 226, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.6f);
            Main.dust[DustID].noGravity = true;
            
            projectile.rotation += 0.1f;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Shocked"), 120);
        }

    }

}

