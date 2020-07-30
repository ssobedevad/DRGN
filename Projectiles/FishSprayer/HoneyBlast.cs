using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace DRGN.Projectiles.FishSprayer
{
    // The following laser shows a channeled ability, after charging up the laser will be fired
    // Using custom drawing, dust effects, and custom collision checks for tiles
    public class HoneyBlast : LaserClass
    {
        public override void SafeSetDefaults()
        {
            FishSprayerType = 3;
        }
        public override void HitEffects(NPC target, int damage, float knockback, bool crit)
        {
            
            target.honeyWet = true;
            target.AddBuff(BuffID.Venom, 120);
            target.AddBuff(BuffID.Midas, 120);
        }
    }
}