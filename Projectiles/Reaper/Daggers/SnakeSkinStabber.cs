using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Reaper.Daggers
{
    public class SnakeSkinStabber : ReaperKnife
    {
        public override void SSD()
        {
            speed = 8.25f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            
        }
    }
    public class SnakeSkinStabberThrown : ReaperKnifeThrown
    {
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            
        }

    }
}
