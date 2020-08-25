using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Reaper.Daggers
{
    public class GoldenDagger : ReaperKnife
    {
        public override void SSD()
        {
            speed = 7.5f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
    }
    public class GoldenDaggerThrown : ReaperKnifeThrown
    {
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }

    }
}
