using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class DragonSlicerProj : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 11;
            projectile.width = 11;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.light = 1.2f;

        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            projectile.velocity.Y += 0.1f;
            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 174, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 1f);
            Main.dust[DustID].noGravity = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600);
            target.AddBuff(BuffID.OnFire, 600);
            target.AddBuff(BuffID.Daybreak, 600);
            if (target.boss == true)
            {
                Main.player[Main.myPlayer].AddBuff(mod.BuffType("BossSlayer"), 360);
            }
            for (int i = 0; i < 10; i++)
            { Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-25, 25), Main.rand.Next(-25, 25), mod.ProjectileType("Spark"), projectile.damage, projectile.knockBack, projectile.owner); }
            Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("FlareExplosion"), projectile.damage, 0f, projectile.owner);
            base.OnHitNPC(target, damage, knockBack, crit);
        }


    }
}
