using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class IceShard : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 8;
            projectile.width = 8;
            projectile.aiStyle = 1;
            projectile.hostile = true;
            projectile.scale = 2f;
            projectile.penetrate = -1;


            projectile.tileCollide = true;

        }
        


        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Frozen, 20 );

        }




    }
}


