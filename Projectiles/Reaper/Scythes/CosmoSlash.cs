using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Reaper.Scythes
{
    public class CosmoSlash : ReaperScythe
    {
        public override void SSD()
        {
            RetractSpeed = 12f;
            OutTime = 75;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("CelestialSwarm"), damage, knockback, projectile.owner);
        }
    }
    public class CosmoSlashThrown : ReaperScytheThrown
    {
        public override void SSD()
        {
            RetractSpeed = 10f;
            OutTime = 70;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("CelestialSwarm"), damage, knockback, projectile.owner);
        }

    }
}
