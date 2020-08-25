using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class GlacialHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 11f;
            range = 600;
            OutTime = 35;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainGlacial";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-15, 15), Main.rand.Next(-15, 15), mod.ProjectileType("IceShatter"), damage, knockback, projectile.owner);
            target.AddBuff(BuffID.Frostburn, 60);
        }
    }
}
