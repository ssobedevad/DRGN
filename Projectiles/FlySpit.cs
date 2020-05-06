using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class FlySpit : ModProjectile
    {
        private bool death;
        public override void SetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 8;
           
            projectile.tileCollide = true;

        }
        public override void AI()
        {
            projectile.velocity.Y += 0.2f;
            if (death == true)
            {
                if (projectile.frame < Main.projFrames[projectile.type] + 1)
                { projectile.frame += 1; }
                else
                {
                    projectile.active = false;
                    for (int i = 0; i < 4; i++)
                    {
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.Bee, projectile.damage, projectile.knockBack, Main.myPlayer);
                    }
                }


            }
        }
        public override bool OnTileCollide(Vector2 Vc)
        {
            death = true;
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.velocity.X = 0;
            projectile.velocity.Y = 0;
            death = true;
        }




    }
}


