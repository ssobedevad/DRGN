using Terraria;

namespace DRGN.Projectiles.Reaper.Hooks
{
    public class SnakeHook : ReaperChain
    {
        public override void SSD()
        {
            RetractSpeed = 10f;
            range = 500;
            OutTime = 28;
            ChainTexturePath = "DRGN/Projectiles/Reaper/Chains/ReaperChainSnake";
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
    }
}
