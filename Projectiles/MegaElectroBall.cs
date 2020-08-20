using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace DRGN.Projectiles
{
    public class MegaElectroBall : ModProjectile
    {
        private int[,] shootAngles = new int[4, 2] { { 0, 8 }, { 8, 0 }, { 0, -8 }, { -8, 0 }, };
        public override void SetDefaults()
        {
            
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
            int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 2f);
            Main.dust[DustID].noGravity = true;
            projectile.rotation += 0.2f;
        }
        public override void Kill(int timeLeft)
        {
            if (Main.netMode != 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    int projid = Projectile.NewProjectile(projectile.Center, new Vector2(shootAngles[i, 0], shootAngles[i, 1]), mod.ProjectileType("ElectroBall"), projectile.damage / 5, projectile.knockBack, Main.myPlayer);
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Shocked"), 60);
        }
    }
}
