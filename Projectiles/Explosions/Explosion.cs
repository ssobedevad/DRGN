using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles.Explosions
{
    public abstract class Explosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }
        public virtual void SetExtraDefaults()
        { }
        public override void SetDefaults()
        {
            projectile.height = 90;
            projectile.width = 56;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;           
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 20;
            projectile.alpha = 120;
            SetExtraDefaults();
        }
        public override void AI()
        {
            projectile.ai[1] += 1;
            if (projectile.ai[1] % 5 == 0)
            {
                if (projectile.frame == 3) { projectile.active = false; }
                else
                {
                    projectile.alpha -= 30;
                    projectile.frame += 1;
                }
            }
        }
    }
}


