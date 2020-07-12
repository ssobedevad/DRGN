using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.Projectiles
{
    public class DragonFireballProjHostile : ModProjectile
    {
    
        public override void SetDefaults()
        {
            
            projectile.height = 64;
            projectile.width = 64;
            projectile.aiStyle = 0;
            projectile.hostile = true;
            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            
            

        }

        public override void AI()
        {
            projectile.rotation += 0.3f;

            
            if (Main.rand.Next(2) == 0)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 35, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 2f); 
                Main.dust[DustID].noGravity = true; 
            }
        

        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Burning"), 60);
        }

    }
}
