using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class FireBallBouncyFriendly : ModProjectile
    {
        private int bounces;
        public override void SetDefaults()
        {

            projectile.height = 31;
            projectile.width = 30;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            
            bounces = 0;
            projectile.tileCollide = true;

        }
        public override void AI()
        {
            if (Main.rand.Next(2) == 0)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 35, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 2f);
                Main.dust[DustID].noGravity = true;
            }
        }
        public override bool OnTileCollide(Vector2 Vc)
        {
            if (bounces < 1)
            { projectile.velocity *= -1;bounces += 1; return false; }

            else
            {
                return true;
            }
        }
      public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Burning"), 120);
        }





    }
}


