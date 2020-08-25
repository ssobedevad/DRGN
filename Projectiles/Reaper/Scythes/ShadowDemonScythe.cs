using Terraria;

namespace DRGN.Projectiles.Reaper.Scythes
{
    public class ShadowDemonScythe : ReaperScythe
    {
        public override void SSD()
        {
            RetractSpeed = 9f;
            OutTime = 60;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
    }
    public class ShadowDemonScytheThrown : ReaperScytheThrown
    {
        public override void SSD()
        {
            RetractSpeed = 7f;
            OutTime = 55;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }

    }
}
