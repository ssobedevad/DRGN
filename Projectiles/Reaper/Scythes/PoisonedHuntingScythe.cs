using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Reaper.Scythes
{
    public class PoisonedHuntingScythe : ReaperScythe
    {
        public override void SSD()
        {
            RetractSpeed = 9.25f;
            OutTime = 65;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 60);
        }
    }
    public class PoisonedHuntingScytheThrown : ReaperScytheThrown
    {
        public override void SSD()
        {
            RetractSpeed = 7.25f;
            OutTime = 60;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 60);
        }

    }
}
