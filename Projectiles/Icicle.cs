using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class Icicle : ModProjectile
    {
       
        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle =-1;
            projectile.friendly = true;

            projectile.penetrate = 1;


            projectile.tileCollide = true;
            
        }
       public override void AI()
       { projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2; projectile.velocity.Y += 0.5f; }


        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, Main.myPlayer);

        }




    }
}


