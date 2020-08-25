using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Reaper.Scythes
{
    public class LunarReaver : ReaperScythe
    {
        public override void SSD()
        {
            RetractSpeed = 14f;
            OutTime = 75;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("LunarExplosion"), damage, 0f, projectile.owner);
        }
    }
    public class LunarReaverThrown : ReaperScytheThrown
    {
        public override void SSD()
        {
            RetractSpeed = 12f;
            OutTime = 70;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("LunarExplosion"), damage, 0f, projectile.owner);
        }

    }
}
