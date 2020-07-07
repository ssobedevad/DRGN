using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class WaterBoltProj : ModProjectile
    {
        
        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = 8;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            
            projectile.tileCollide = true;

        }
       
        public override bool OnTileCollide(Vector2 Vc)
        {
            for (int i = 0; i < 2; i++)
            {
                
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, Main.myPlayer);
            }
                


            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 2; i++)
            {

                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, Main.myPlayer);
            }

        }





    }
}
