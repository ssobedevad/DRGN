using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class BinaryShot : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 2;
            projectile.alpha = 60;
            projectile.tileCollide = true;
            
        }
        public override void AI()
        {

            if (projectile.ai[1] == 0)
            {

                int sprite = ((Main.rand.Next(0, 2)));
                projectile.frame = sprite;
                projectile.ai[1] += 1;
            }
            
            int dustid = Dust.NewDust(projectile.Center, projectile.height, projectile.width, 107);
            Main.dust[dustid].noGravity = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Bugged"), 30);
        }



    }
}


