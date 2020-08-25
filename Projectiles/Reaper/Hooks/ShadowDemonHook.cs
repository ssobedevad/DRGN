using Terraria;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class ShadowDemonHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 9f;
            range = 450;
            OutTime = 24;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainShadow";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
    }
}
