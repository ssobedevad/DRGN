using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class Marshmallow : ModProjectile
    {
        
        private NPC hitEnemy = null;
        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            
            projectile.penetrate = 6;


        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();
            if (hitEnemy != null) { if (hitEnemy.active) { projectile.position = hitEnemy.position; } else { projectile.active = false; } }
             
                    
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (hitEnemy == null)
            { 
                hitEnemy = target;
                projectile.knockBack = 0f;
            }
            
        }


    }

}

