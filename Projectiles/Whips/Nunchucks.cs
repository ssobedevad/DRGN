using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


using Terraria;

namespace DRGN.Projectiles.Whips
{

    public class Nunchucks : WhipClass
    {

        public override void SafeSetDefaults()
        {
            

            rangeMult = 0.45f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
           if(Main.rand.Next(0,100) < player.HeldItem.crit)
            { crit = true; }
        }




    }
}
