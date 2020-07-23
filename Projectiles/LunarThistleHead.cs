using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System;

namespace DRGN.Projectiles
{
    public class LunarThistleHead : ThornWeapon
    {


        public override void SafeSetDefaults()
        {

            projectile.height = 34;
            projectile.width = 34;
            head = true;
            BodyType = mod.ProjectileType("LunarThistleBody");
            Length = 15;
            fadeSpeed = 8;
            partOffset = 34;


        }


    }

}