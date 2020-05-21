using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace DRGN.Projectiles
{
    public class MegaElectroBall : ModProjectile
    {
        private int[,] shootAngles = new int[8, 2] { { 0, 8 }, { 4, 4 }, { 8, 0 }, { 4, -4 }, { 0, -8 }, { -4, -4 }, { -8, 0 }, { -4, 4 } };
        public override void SetDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = 0;
            projectile.hostile = true;
            projectile.ranged = true;
            projectile.timeLeft = 90;
            projectile.penetrate = -1;
            projectile.tileCollide = false;

        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];


            }
            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 226, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 2f);
            Main.dust[DustID].noGravity = true;

            projectile.rotation += 0.1f;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Projectile.NewProjectile(projectile.Center, new Vector2(shootAngles[i,0], shootAngles[i, 1]), mod.ProjectileType("ElectroBall"), projectile.damage/5, projectile.knockBack, Main.myPlayer);

            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Shocked"), 120);
        }
    }
}
