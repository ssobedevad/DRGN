using Terraria;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class GoldenHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 8.5f;
            range = 400;
            OutTime = 20;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainGold";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
    }
}
