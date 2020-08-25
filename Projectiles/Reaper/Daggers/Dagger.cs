using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Reaper.Daggers
{
    public class Dagger : ReaperKnife
    {
        public override void SSD()
        {
            speed = 7f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            
        }
    }
    public class DaggerThrown : ReaperKnifeThrown
    {
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            
        }

    }
}
