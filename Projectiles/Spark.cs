using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class Spark : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 4;
            projectile.width = 4;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;


            projectile.tileCollide = true;

        }



        




    }
}
