using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class GiantSpikyBall : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 56;
            projectile.width = 56;
            projectile.aiStyle = 14;
            projectile.hostile = true;
            
            projectile.penetrate = -1;


            projectile.tileCollide = true;

        }



        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 200);

        }




    }
}


