using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class IcicleBlast : ModProjectile
    {
        private int GravDelay;
        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = 0;
            projectile.friendly = true;

            projectile.penetrate = -1;


            projectile.tileCollide = true;
            GravDelay = 80;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2; // projectile sprite faces up
            if (GravDelay == 0) {  GravDelay = -1; }
            else if (GravDelay == -1) { projectile.velocity.Y += 0.8f; if (projectile.velocity.Y > 16f) { projectile.velocity.Y = 16f; } }
            else { GravDelay -= 1; projectile.velocity.Y += 0.05f;projectile.velocity.X *= 0.98f; }
            

        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-18, 18), Main.rand.Next(-18, 18), ProjectileID.CrystalShard, projectile.damage, projectile.knockBack, Main.myPlayer);

        }




    }
}


