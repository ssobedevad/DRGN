using DRGN.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN
{
    public class whip : Projectiles.Whips.WhipClass
    {

        public override void SafeSetDefaults()
        {
            summonTagDamage = 69696969;
            summonTagCrit = 100;
            rangeMult = 1.5f;
        }

        




    }
}
