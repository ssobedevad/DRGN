using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.Projectiles
{
    public class BlueFire : ModProjectile
    {

        public override void SetDefaults()
        {
            
            projectile.height = 15;
            projectile.width = 15;
            projectile.aiStyle = 0;
            projectile.hostile = true;
            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.damage = 10;
            projectile.timeLeft = 120;

        }

        public override void AI()
        {

            projectile.rotation += 0.3f;
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
