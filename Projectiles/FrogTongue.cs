
using Microsoft.Xna.Framework;

using Terraria;


namespace DRGN.Projectiles
{
    // The following laser shows a channeled ability, after charging up the laser will be fired
    // Using custom drawing, dust effects, and custom collision checks for tiles
    public class FrogTongue : ThrownRetracting
    {
        public override void SafeSetDefaults()
        {
            baseUseSpeed = 155;
            maxDistance = 700;
            heldTextureDimensions = new Vector2(26, 22);
            bodyTextureDimensions = new Vector2(26, 30);
            endTextureDimensions = new Vector2(26, 22);


        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 6;
            retract = true;
            target.AddBuff(mod.BuffType("Melting"), 60);
        }
    }
}
