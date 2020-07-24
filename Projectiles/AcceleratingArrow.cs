using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class AcceleratingArrow : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 12;
            projectile.width = 12;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.hide = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 100;

        }
        
        public override void AI()
        {
            int DustID = Dust.NewDust(projectile.position, projectile.width, projectile.height , 74, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.5f);
            Main.dust[DustID].noGravity = true;
            projectile.velocity *= 1.0001f;
            projectile.damage = (int)(projectile.damage *1.002f);
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)

        {
            projectile.damage = (int)(projectile.damage * 1.08f);
            base.OnHitNPC(target, damage, knockBack, crit);
        }

    }

}

