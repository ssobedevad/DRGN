using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Reaper.Scythes
{
    public class VoidScythe : ReaperScythe
    {
        public override void SSD()
        {
            RetractSpeed = 16f;
            OutTime = 80;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("VoidExplosion"), damage, 0f, projectile.owner);
        }
    }
    public class VoidScytheThrown : ReaperScytheThrown
    {
        public override void SSD()
        {
            RetractSpeed = 14f;
            OutTime = 75;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("VoidExplosion"), damage, 0f, projectile.owner);
        }

    }
}
