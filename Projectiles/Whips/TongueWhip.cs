using DRGN.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Projectiles.Whips
{

    public class TongueWhip : WhipClass
    {

        public override void SafeSetDefaults()
        {
            summonTagDamage = 6;
            summonTagCrit = 2;
            rangeMult = 0.6f;
        }

        public override void NpcEffects(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Melting>(), 120);
        }




    }
}
