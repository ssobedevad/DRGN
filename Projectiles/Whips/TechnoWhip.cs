using DRGN.Buffs;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Whips
{

    public class TechnoWhip : WhipClass
    {

        public override void SafeSetDefaults()
        {
            summonTagDamage = 10;
            summonTagCrit = 4;
            rangeMult = 0.75f;
        }

        public override void NpcEffects(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Bugged"), 80);
        }




    }
}
