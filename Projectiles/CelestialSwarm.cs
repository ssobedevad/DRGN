using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class CelestialSwarm : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 16;
            projectile.width = 16;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 1;
            
           
            projectile.tileCollide = true;
            
            
        }
        public override void AI()
        {

            
            if (Main.rand.Next(0, 12) == 1)
            {
                int Dustid = Dust.NewDust(projectile.Center, 0, 0, DustID.AmberBolt, 0f, 0f, 0, default(Color), 1f);
            }
        }
        



    }
}


