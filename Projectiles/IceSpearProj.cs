﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Projectiles
{
    public class IceSpearProj : ModProjectile
    {
        public override void SetDefaults()
        {

            projectile.height = 13;
            projectile.width = 13;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            projectile.scale = 2f;


        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 600);
            target.AddBuff(BuffID.Chilled, 600);


            base.OnHitNPC(target, damage, knockBack, crit);
        }


    }
}
