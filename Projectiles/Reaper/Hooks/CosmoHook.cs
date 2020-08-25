using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class CosmoHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 12f;
            range = 650;
            OutTime = 45;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainCosmo";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("CelestialSwarm"), damage, knockback, projectile.owner);
        }
    }
}
