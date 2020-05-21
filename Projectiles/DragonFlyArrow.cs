using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class DragonFlyArrow : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;

        }
        
        public override void Kill(int timeleft)
        {
            for (int i = 0; i < 3; i++)
            { Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("DragonFlyJaws"), projectile.damage, projectile.knockBack, projectile.owner); }
        }


    }
}
