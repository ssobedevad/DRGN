using Microsoft.Xna.Framework;

using Terraria;


namespace DRGN.Projectiles
{
    // The following laser shows a channeled ability, after charging up the laser will be fired
    // Using custom drawing, dust effects, and custom collision checks for tiles
    public class AntBiterWinged : ThrownRetracting
    {
        public override void SafeSetDefaults()
        {
            baseUseSpeed = 255;
            maxDistance = 900;
            heldTextureDimensions = new Vector2(80, 30);
            bodyTextureDimensions = new Vector2(80, 32);
            endTextureDimensions = new Vector2(80, 64);


        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 4;
            retract = true;
            for (int i = 0; i < 5; i++)
            { Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("AntBiterJaws"), projectile.damage, projectile.knockBack, projectile.owner); }
        }
    }
}
