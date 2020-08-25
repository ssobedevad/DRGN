using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class DragonFlyHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 13f;
            range = 700;
            OutTime = 50;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainDragonFly";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("DragonFlyJaws"), damage, knockback, projectile.owner);
        }
    }
}
