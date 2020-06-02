using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class Bomb : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 44;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            
            projectile.penetrate = 1;
            
           
            projectile.tileCollide = true;

        }
        public override void AI()
        {
            projectile.velocity.Y += 1.2f;
            if(projectile.velocity.Y >= 16) { projectile.velocity.Y = 16; }
        }
        public override void Kill(int timeLeft)
        {
            
            Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("FlareExplosion"), projectile.damage, 0f, projectile.owner);



        }

        




    }
}


