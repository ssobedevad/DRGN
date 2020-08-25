using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class AntHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 10.5f;
            range = 550;
            OutTime = 32;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainAnt";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("AntBiterJaws"), damage, knockback, projectile.owner);
        }
    }
}
