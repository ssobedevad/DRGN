using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Reaper.Scythes
{
    public class RockSlicer : ReaperScythe
    {
        public override void SSD()
        {
            RetractSpeed = 10f;
            OutTime = 75;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), ModContent.ProjectileType<RockShot>(), damage, knockback, projectile.owner);
        }
    }
    public class RockSlicerThrown : ReaperScytheThrown
    {
        public override void SSD()
        {
            RetractSpeed = 8f;
            OutTime = 70;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), ModContent.ProjectileType<RockShot>(), damage, knockback, projectile.owner);
        }

    }
}
