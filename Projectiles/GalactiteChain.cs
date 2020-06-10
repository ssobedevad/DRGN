

    
using Microsoft.Xna.Framework;

using Terraria;


namespace DRGN.Projectiles
{
    // The following laser shows a channeled ability, after charging up the laser will be fired
    // Using custom drawing, dust effects, and custom collision checks for tiles
    public class GalactiteChain : ThrownRetracting
    {
        public override void SafeSetDefaults()
        {
            baseUseSpeed = 850;
            maxDistance = 2000;
            heldTextureDimensions = new Vector2(28, 26);
            bodyTextureDimensions = new Vector2(28, 26);
            endTextureDimensions = new Vector2(28, 26);
            

        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            retract = true;
            target.AddBuff(mod.BuffType("GalacticCurse"), 120);
            Projectile.NewProjectile(target.Center + new Vector2(Main.rand.Next(-5, 5), -1000), new Vector2(0, Main.rand.Next(1, 5)), mod.ProjectileType("OmegaBeeStar"), projectile.damage, 0f, projectile.owner, projectile.Center.Y - 10);
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("GalacticExplosion"), projectile.damage, 0f, projectile.owner);

        }
    }
}
