using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class SecurityBreach : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;

            projectile.penetrate = 1;


            projectile.tileCollide = true;

        }
        public override void AI()
        {
            projectile.velocity.Y += 0.25f;
            
            if (projectile.velocity.Y >= 16) { projectile.velocity.Y = 16; }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Projectile.NewProjectile(projectile.Center, new Vector2(Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-10f, 10f)), mod.ProjectileType("BinaryShot"), projectile.damage, 0f, projectile.owner);
            }



        }






    }
}


