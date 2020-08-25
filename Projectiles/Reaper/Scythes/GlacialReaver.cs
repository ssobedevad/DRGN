using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Reaper.Scythes
{
    public class GlacialReaver : ReaperScythe
    {
        public override void SSD()
        {
            RetractSpeed = 10f;
            OutTime = 60;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-15, 15), Main.rand.Next(-15, 15), mod.ProjectileType("IceShatter"), damage, knockback, projectile.owner);
            target.AddBuff(BuffID.Frostburn, 60);
        }
    }
    public class GlacialReaverThrown : ReaperScytheThrown
    {
        public override void SSD()
        {
            RetractSpeed = 8f;
            OutTime = 55;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-15, 15), Main.rand.Next(-15, 15), mod.ProjectileType("IceShatter"), damage, knockback, projectile.owner);
            target.AddBuff(BuffID.Frostburn, 60);
        }

    }
}
