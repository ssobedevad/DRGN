using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Reaper.Scythes
{
    public class FlareReaver : ReaperScythe
    {
        public override void SSD()
        {
            RetractSpeed = 15f;
            OutTime = 80;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("FlareExplosion"), damage, 0f, projectile.owner);
            target.AddBuff(BuffID.Daybreak, 60);
        }
    }
    public class FlareReaverThrown : ReaperScytheThrown
    {
        public override void SSD()
        {
            RetractSpeed = 13f;
            OutTime = 75;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("FlareExplosion"), damage, 0f, projectile.owner);
            target.AddBuff(BuffID.Daybreak, 60);
        }

    }
}
