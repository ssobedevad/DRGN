using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class EngineerBullet : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 8;
            projectile.width = 8;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.melee = false;
            projectile.ranged = false;
            projectile.magic = false;
            projectile.thrown = false;
            projectile.minion = false;
            projectile.ai[0] = 1;
            projectile.penetrate = (int)projectile.ai[0];
            projectile.ai[1] = 0;

        }

        public override void AI()
        {
            projectile.penetrate = (int)projectile.ai[0];
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        { 
        if (Main.rand.Next(0, 100)<=(int)(projectile.ai[1]))
        {
                crit = true;


        }
        
        
        }
    }

}

