using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class DiamondBouncy : ModProjectile
    {
        private int bounces;

        

        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            
            projectile.penetrate = 3;
            
            bounces = 0;
            projectile.tileCollide = true;
            

        }
        public override void AI()
        {
            projectile.rotation += 0.3f;
            if (Main.rand.Next(2) == 0)
            {
                int Dustid = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 91, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 1f);
                Main.dust[Dustid].noGravity = true;
            }
            

        }
        public override bool OnTileCollide(Vector2 Oldvel)
        {
            if (bounces < 2)
            {
                if (projectile.velocity.Y != Oldvel.Y)
                {
                    projectile.velocity.Y = 0f - Oldvel.Y;
                }
                if (projectile.velocity.X != Oldvel.X)
                {
                    projectile.velocity.X = 0f - Oldvel.X;
                }
                bounces += 1; return false;
            }

            else
            {
                return true;
            }
        }
        





    }
}


