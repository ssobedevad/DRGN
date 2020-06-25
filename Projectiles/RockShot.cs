using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class RockShot : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;

        }
        public override void AI()
        {
            int dustid = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.t_Lihzahrd);
            Main.dust[dustid].noGravity = true;
        }

        


    }
}
