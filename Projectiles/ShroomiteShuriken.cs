using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles
{
    public class ShroomiteShuriken : ModProjectile
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
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)

        {

            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-20, 20) - (projectile.velocity.X * 25), projectile.Center.Y + Main.rand.Next(-30, 10) - (projectile.velocity.Y * 25), (float)(projectile.velocity.X * 1.7) + Main.rand.Next(-2, 2), (float)(projectile.velocity.Y * 1.7) + Main.rand.Next(-2, 2), mod.ProjectileType("ShroomiteShurikenGhost"), projectile.damage, projectile.knockBack, Main.myPlayer, target.whoAmI);
            }


        }


    }
}