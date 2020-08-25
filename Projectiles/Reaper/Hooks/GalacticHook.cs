using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class GalacticHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 19f;
            range = 900;
            OutTime = 60;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainGalactic";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("GalacticCurse"), 120);
            Projectile.NewProjectile(target.Center + new Vector2(Main.rand.Next(-5, 5), -1000), new Vector2(0, Main.rand.Next(1, 5)), mod.ProjectileType("OmegaBeeStar"), damage, 0f, projectile.owner, target.Center.Y - 10);
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("GalacticExplosion"), damage, 0f, projectile.owner);
        }
    }
}
