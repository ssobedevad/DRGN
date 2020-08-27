using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class DragonBladeProj : ModProjectile
    {
        public override void SetDefaults()
        {
        
            projectile.height = 22;
            projectile.width = 22;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;


        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {
          
            target.AddBuff(BuffID.Daybreak, 600);


            Projectile.NewProjectile(target.Center.X, target.Top.Y - 5, 0, -12, mod.ProjectileType("DragonBladeEx"), projectile.damage, projectile.knockBack, projectile.owner);
            Projectile.NewProjectile(target.Center.X, target.Bottom.Y + 5, 0, 12, mod.ProjectileType("DragonBladeEx"), projectile.damage, projectile.knockBack, projectile.owner);
            Projectile.NewProjectile(target.Left.X - 10, target.Center.Y, -12, 0, mod.ProjectileType("DragonBladeEx"), projectile.damage, projectile.knockBack, projectile.owner);
            Projectile.NewProjectile(target.Right.X + 10, target.Center.Y, 12, 0, mod.ProjectileType("DragonBladeEx"), projectile.damage, projectile.knockBack, projectile.owner);
            Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("FlareExplosion"), projectile.damage, 0f, projectile.owner);


            if (target.boss == true )
                Main.player[projectile.owner].AddBuff(mod.BuffType("BossSlayer"), 360);
            
        }
        public override void AI()
        {
            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 174, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 1f);
            Main.dust[DustID].noGravity = true;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
        }
        


    }
}
