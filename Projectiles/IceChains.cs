




    
using Microsoft.Xna.Framework;

using Terraria;


namespace DRGN.Projectiles
{
    // The following laser shows a channeled ability, after charging up the laser will be fired
    // Using custom drawing, dust effects, and custom collision checks for tiles
    public class IceChains : ThrownRetracting
    {
        public override void SafeSetDefaults()
        {
            baseUseSpeed = 300;
            maxDistance = 1000;
            heldTextureDimensions = new Vector2(18, 20);
            bodyTextureDimensions = new Vector2(18, 32);
            endTextureDimensions = new Vector2(18, 22);


        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 3;
            retract = true;
            for (int i = 0; i < 3; i++)
            { Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("IceShatter"), projectile.damage, projectile.knockBack, projectile.owner); }

        }
    }
}
