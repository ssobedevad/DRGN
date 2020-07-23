using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System;

namespace DRGN.Projectiles
{
    public class LunarThistleBody : ThornWeapon
    {
        public override void SafeSetDefaults()
        {

            projectile.height = 34;
            projectile.width = 34;
            head = false;


            fadeSpeed = 8;



        }
    }

}