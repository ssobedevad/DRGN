using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace DRGN.Projectiles
{
    public class MegaElectroBallFriendly : ModProjectile
    {
        private int[,] shootAngles = new int[4, 2] { { 0, 8 }, { 8, 0 }, { 0, -8 }, { -8, 0 }, };
        public override void SetDefaults()
        {
           
            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 40;
            projectile.penetrate = -1;
            projectile.tileCollide = false;

        }

        public override void AI()
        {
            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 2f);
            Main.dust[DustID].noGravity = true;

            projectile.rotation += 0.1f;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 4; i++)
            {
                Projectile.NewProjectile(projectile.Center, new Vector2(shootAngles[i,0], shootAngles[i, 1]), mod.ProjectileType("ElectroBallFriendly"), projectile.damage, projectile.knockBack, Main.myPlayer);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Shocked"), 60);
        }
    }
}
