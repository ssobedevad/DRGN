using Terraria;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class IronHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 8f;
            range = 350;
            OutTime = 15;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainIron";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
    }
}
