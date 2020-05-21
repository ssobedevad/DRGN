using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace DRGN.Projectiles
{
    public class VoidArrow : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 32;
            projectile.width = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;


        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)

        {
            target.AddBuff(mod.BuffType("VoidBuff"), 120);
            base.OnHitNPC(target, damage, knockBack, crit);
        }
       
    }

}

