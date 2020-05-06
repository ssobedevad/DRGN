using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class GemshotProj : ModProjectile
    {
        public override void SetDefaults()
        {
            
            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            Main.projFrames[projectile.type] = 3;
            projectile.ai[1] = 0;
            projectile.tileCollide = true;
            
        }
        public override void AI()
        {
            if (projectile.ai[1] == 0)
            {

                int sprite = ((Main.rand.Next(0, 3)));
                projectile.frame = sprite;
                projectile.ai[1] += 1;
            }
        }
        public override bool OnTileCollide(Vector2 Vc)
            { Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, Main.myPlayer);


            return true;
            }

        public override void OnHitNPC(NPC target, int damage , float knockback, bool crit)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, Main.myPlayer);
            
            }

            
        
        
    }
}
    

