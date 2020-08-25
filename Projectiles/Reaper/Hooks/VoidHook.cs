using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class VoidHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 16f;
            range = 800;
            OutTime = 58;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainVoid";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("VoidExplosion"), damage, 0f, projectile.owner);
        }
    }
}
