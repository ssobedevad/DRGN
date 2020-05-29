using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class FlareShuriken : ModProjectile
    {
        

        
        
        
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 1;
            
            
            
            

        }
        public override void AI()
        {



          
           
            projectile.rotation += 1f;
            projectile.velocity.Y += 0.1f;



            
        }
        public override void Kill(int timeleft)
        { Projectile.NewProjectile(projectile.Center + projectile.velocity, Vector2.Zero, mod.ProjectileType("FlareExplosion"), projectile.damage/2, 0f, projectile.owner); }


    }
}