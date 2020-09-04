using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class FireOrbSplit : ModProjectile
    {
        private int VolleyCD;

        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 5;
            projectile.ai[0] = 0;
            projectile.ai[1] = 0;
            projectile.tileCollide = true;


        }
        public override void AI()
        {
            projectile.rotation += 0.3f;
            if (Main.rand.Next(2) == 0)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 35, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 1f);
                Main.dust[DustID].noGravity = true;
            }
        }
        public override bool OnTileCollide(Vector2 Oldvel)
        {
            if (projectile.ai[1] < 5)
            {
                if (projectile.velocity.Y != Oldvel.Y)
                {
                    projectile.velocity.Y = 0f - Oldvel.Y;
                }
                if (projectile.velocity.X != Oldvel.X)
                {
                    projectile.velocity.X = 0f - Oldvel.X;
                }
                projectile.ai[1] += 1; return false;
            }
            else
            {
                return true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
        }
    }
}


