using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class VoidSeeker : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 13;
            projectile.width = 13;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            projectile.scale = 2.4f;


        }
        public override void AI()
        {
            if (Main.rand.Next(0, 2) == 1)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 1, projectile.height + 1, 98, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 200, default(Color), 1f);
                Main.dust[DustID].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 2;
        }



    }
}
