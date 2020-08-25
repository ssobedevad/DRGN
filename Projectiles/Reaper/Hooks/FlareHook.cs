using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class FlareHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 14.5f;
            range = 650;
            OutTime = 55;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainFlare";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("FlareExplosion"), damage, 0f, projectile.owner);
            target.AddBuff(BuffID.Daybreak, 60);
        }
    }
}
