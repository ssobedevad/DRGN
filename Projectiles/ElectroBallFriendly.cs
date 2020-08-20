using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class ElectroBallFriendly : ModProjectile
    {
        
        public override void SetDefaults()
        {
            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 220;
            projectile.penetrate = 1;         

        }

        public override void AI()
        {            
            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.6f);
            Main.dust[DustID].noGravity = true;          
            projectile.rotation += 0.3f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Shocked"),60);
        }

    }

}

