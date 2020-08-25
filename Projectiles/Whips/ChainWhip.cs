using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


using Terraria;

namespace DRGN.Projectiles.Whips
{

    public class ChainWhip : WhipClass
    {
        public override void SafeSetDefaults()
        {
            summonTagDamage = 2;
            rangeMult = 0.4f;
        }       
    }
}
