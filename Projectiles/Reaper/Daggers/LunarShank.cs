using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Reaper.Daggers
{
    public class LunarShank : ReaperKnife
    {
        public override void SSD()
        {
            speed = 10.5f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("LunarExplosion"), damage, 0f, projectile.owner);
        }
    }
    public class LunarShankThrown : ReaperKnifeThrown
    {
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("LunarExplosion"), damage, 0f, projectile.owner);
        }

    }
}
