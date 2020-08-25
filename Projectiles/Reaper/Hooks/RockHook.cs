using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class RockHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 12f;
            range = 650;
            OutTime = 45;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainRock";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), ModContent.ProjectileType<RockShot>(), damage, knockback, projectile.owner);
        }
    }
}
