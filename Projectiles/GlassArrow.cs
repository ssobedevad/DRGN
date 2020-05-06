using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace DRGN.Projectiles
{
    public class GlassArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            
            projectile.height = 32;
            projectile.width = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;


        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)

        {
            for (int i = 0; i < Main.rand.Next(1, 4); i++)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y + Main.rand.Next(-5, 5), mod.ProjectileType("GlassShatter"), projectile.damage, projectile.knockBack, Main.myPlayer);
            }
            base.OnHitNPC(target, damage, knockBack, crit);
        }
        public override void AI()
        {
            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 226, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.5f);
            Main.dust[DustID].noGravity = true;
        }
        
    }

}

