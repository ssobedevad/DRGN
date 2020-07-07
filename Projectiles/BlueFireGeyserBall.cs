using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.Projectiles
{
    public class BlueFireGeyserBall : ModProjectile
    {
        private int[,] shootAngles = new int[8, 2] { { 0, 8 }, { 4, 4 }, { 8, 0 }, { 4, -4 }, { 0, -8 }, { -4, -4 }, { -8, 0 }, { -4, 4 } };
        private int shootCD;
        private int shootDir;
        public override void SetDefaults()
        {
            
            projectile.height = 63;
            projectile.width = 60;
            projectile.aiStyle = 0;
            projectile.hostile = true;
            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            
            projectile.timeLeft = 120;
            shootCD = 0;
            shootDir = 0;
        }

        public override void AI()
        {
            if (shootCD > 0) { shootCD -= 1; }
            if (shootDir == 8) { shootDir = 0; }
            projectile.rotation += 0.3f;
            if (shootCD == 0) { Projectile.NewProjectile(projectile.Center, new Vector2(shootAngles[shootDir,0], shootAngles[shootDir, 1]), mod.ProjectileType("BlueFire"), projectile.damage / 3, 0f);  shootCD = 18;shootDir+=1;  }
            if (Main.rand.Next(0, 2) == 1)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 98, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 0, default(Color), 2f);
                Main.dust[DustID].noGravity = true;


            }

        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("BrokenWings"), 60);
        }

    }
}
