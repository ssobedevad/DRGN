using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class ToxicFang : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 8;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            
        }

        public override void AI()
        {
            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 74, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.6f);
            Main.dust[DustID].noGravity = true;
            
        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)

        {

            target.AddBuff(mod.BuffType("Melting"), 600);
            base.OnHitNPC(target, damage, knockBack, crit);
        }

    }

}


