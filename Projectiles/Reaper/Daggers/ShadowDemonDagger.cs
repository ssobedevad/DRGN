using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Reaper.Daggers
{
    public class ShadowDemonDagger : ReaperKnife
    {
        public override void SSD()
        {
            speed = 8f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
    }
    public class ShadowDemonDaggerThrown : ReaperKnifeThrown
    {
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }

    }
}
