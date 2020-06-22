using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
namespace DRGN.Projectiles
{
    public class VoidLazer : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;


        }
        public override void AI()
        { projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(0, 2) == 1)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 98, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 0, default(Color), 2f);
                Main.dust[DustID].noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 98, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 0, default(Color), 4f);
                Main.dust[DustID].noGravity = true;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Webbed"), 60);
            target.immuneTime = 1;
            projectile.active = false;
        }

    }

}

