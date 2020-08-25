using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Reaper.Daggers
{
    public class PoisonedHuntingKnife : ReaperKnife
    {
        public override void SSD()
        {
            speed = 8.5f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 60);
        }
    }
    public class PoisonedHuntingKnifeThrown : ReaperKnifeThrown
    {
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 60);
        }

    }
}
