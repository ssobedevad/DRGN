using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Reaper.Scythes
{
    public class AntSlasher : ReaperScythe
    {
        public override void SSD()
        {
            RetractSpeed = 9.25f;
            OutTime = 68;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("AntBiterJaws"), damage, knockback, projectile.owner);
        }
    }
    public class AntSlasherThrown : ReaperScytheThrown
    {
        public override void SSD()
        {
            RetractSpeed = 7.25f;
            OutTime = 63;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("AntBiterJaws"), damage, knockback, projectile.owner);
        }

    }
}
