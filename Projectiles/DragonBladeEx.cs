using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class DragonBladeEx : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 8;
            projectile.width = 8;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 1;


            projectile.tileCollide = true;

        }








    }
}
