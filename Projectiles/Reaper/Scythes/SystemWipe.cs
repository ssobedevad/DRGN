using Terraria;
using Terraria.ID;

namespace DRGN.Projectiles.Reaper.Scythes
{
    public class SystemWipe : ReaperScythe
    {
        public override void SSD()
        {
            RetractSpeed = 10f;
            OutTime = 65;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Bugged"), 90);
        }
    }
    public class SystemWipeThrown : ReaperScytheThrown
    {
        public override void SSD()
        {
            RetractSpeed = 8f;
            OutTime = 60;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Bugged"), 90);
        }

    }
}
