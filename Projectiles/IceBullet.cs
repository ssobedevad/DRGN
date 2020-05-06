using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class IceBullet : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 8;
            projectile.width = 8;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.hide = true;
            projectile.penetrate = 3;


        }

        public override void AI()
        {
            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 185, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 2f);
            Main.dust[DustID].noGravity = true;

        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)

        {
            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 185, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 5f);
            Main.dust[DustID].noGravity = true;
            for (int i = 0; i < 10; i++)
            { Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-25, 25), Main.rand.Next(-25, 25), mod.ProjectileType("IceShatter"), projectile.damage, projectile.knockBack,projectile.owner); }
            base.OnHitNPC(target, damage, knockBack, crit);
        }

    }

}

