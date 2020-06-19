using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class Bulb : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 24;
            projectile.width = 24;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.timeLeft = 300;
            projectile.penetrate = -1;


        }

        public override void AI()
        {
            if (Main.rand.Next(5) == 1)
            {
                int Dustid = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, DustID.PinkFlame, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 1f);
                Main.dust[Dustid].noGravity = true;
            }
            projectile.rotation += 0.1f;
            for (int i = 0; i < 200; i ++)
            { if (Vector2.Distance(projectile.Center, Main.player[i].Center) < 40) { projectile.timeLeft = 1; Main.player[i].statLife += 5; Main.player[i].HealEffect(5); } }
            

        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int Dustid = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, DustID.PinkFlame, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 2f);
                Main.dust[Dustid].noGravity = true;
            }
        }
      

    }

}

