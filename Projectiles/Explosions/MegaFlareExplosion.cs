﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles.Explosions
{
    public class MegaFlareExplosion : Explosion
    {
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 120);           
        }
        public override void SetExtraDefaults()
        {
            projectile.height = 180;
            projectile.width = 112;
        }
    }
}