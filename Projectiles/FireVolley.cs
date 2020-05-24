using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class FireVolley : ModProjectile
    {
        private int bounces;
        
        private int VolleyCD;

        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 3;
            projectile.ai[0] = 0;
            bounces = 0;
            projectile.tileCollide = true;
            VolleyCD = 0;
            
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2; // projectile sprite faces up
            if (Main.rand.Next(2) == 0)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 35, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 2f);
                Main.dust[DustID].noGravity = true;
            }
            if (projectile.ai[0] > 0 && VolleyCD > 5)
            { Projectile.NewProjectile(Main.player[projectile.owner].Center, projectile.velocity, mod.ProjectileType("FireVolley"), projectile.damage, projectile.knockBack, projectile.owner); projectile.ai[0] -= 1;VolleyCD = 0; }
            VolleyCD += 1;

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


