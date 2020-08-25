using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class LunarHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 12.5f;
            range = 675;
            OutTime = 48;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainLunar";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("LunarExplosion"), damage, 0f, projectile.owner);
        }
    }
}
