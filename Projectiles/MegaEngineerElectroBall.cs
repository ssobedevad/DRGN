using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace DRGN.Projectiles
{
    public class MegaEngineerElectroBall : ModProjectile
    {
        private int[,] shootAngles = new int[8, 2] { { 0, 8 }, { 4, 4 }, { 8, 0 }, { 4, -4 }, { 0, -8 }, { -4, -4 }, { -8, 0 }, { -4, 4 } };
        public override void SetDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.melee = false;
            projectile.ranged = false;
            projectile.magic = false;
            projectile.thrown = false;
            projectile.minion = false;
            projectile.ai[0] = 1;
            projectile.penetrate = (int)projectile.ai[0];
            projectile.ai[1] = 0;

        }

        public override void AI()
        {
            projectile.penetrate = (int)projectile.ai[0];
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
                Projectile.NewProjectile(projectile.Center, new Vector2(shootAngles[i,0], shootAngles[i, 1]), mod.ProjectileType("EngineerElectroBall"), projectile.damage, projectile.knockBack, Main.myPlayer,projectile.ai[0], projectile.ai[1]);

            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Shocked"), 120);
        }
    }
}
