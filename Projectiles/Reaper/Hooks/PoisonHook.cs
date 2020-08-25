using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class PoisonHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 10.25f;
            range = 525;
            OutTime = 30;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainPoison";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 60);
        }
    }
}
