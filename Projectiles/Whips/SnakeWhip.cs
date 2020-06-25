using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


using Terraria;

namespace DRGN.Projectiles.Whips
{

    public class SnakeWhip : WhipClass
    {

        public override void SafeSetDefaults()
        {
            summonTagDamage = 4;
            
            rangeMult = 0.5f;
        }

        public override void NpcEffects(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 100);
        }




    }
}
