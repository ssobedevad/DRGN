using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class TechnoHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 11.5f;
            range = 625;
            OutTime = 38;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainTechno";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Bugged"), 90);
        }
    }
}
