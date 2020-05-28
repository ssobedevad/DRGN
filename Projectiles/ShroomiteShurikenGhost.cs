using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class ShroomiteShurikenGhost : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 22;
            projectile.width = 22;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            


        }
        public override void AI()
        {
            projectile.rotation += 1f;
            projectile.velocity.Y += 0.1f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 2;
        }



    }
}
