using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System;

namespace DRGN.Projectiles
{
    public class MagmaticSpikeHead : ThornWeapon
    {


        public override void SafeSetDefaults()
        {

            projectile.height = 34;
            projectile.width = 34;
            head = true;
            BodyType = mod.ProjectileType("MagmaticSpikeBody");
            Length = 25;
            fadeSpeed = 10;
            partOffset = 34;


        }


    }

}