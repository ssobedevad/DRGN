using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Reaper.Daggers
{
    public class FlareStabber : ReaperKnife
    {
        public override void SSD()
        {
            speed = 12f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("FlareExplosion"), damage, 0f, projectile.owner);
            target.AddBuff(BuffID.Daybreak, 60);
        }
    }
    public class FlareStabberThrown : ReaperKnifeThrown
    {
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("FlareExplosion"), damage, 0f, projectile.owner);
            target.AddBuff(BuffID.Daybreak, 60);
        }

    }
}
