using DRGN.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Whips
{
    public class VoidSnakeWhip : WhipClass
    {

        public override void SafeSetDefaults()
        {
            summonTagDamage = 150;
            summonTagCrit = 45;
            rangeMult = 1.6f;
        }

        public override void NpcEffects(NPC target, int damage, float knockback, bool crit)
        {


            target.AddBuff(ModContent.BuffType<VoidBuff>(), 180);
        }




    }
}
