using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System;

namespace DRGN.Projectiles
{
    public class BeetleThornHead : ThornWeapon
    {
		

		public override void SafeSetDefaults()
        {
          
            projectile.height = 34;
            projectile.width = 34;
            head = true;
            BodyType = mod.ProjectileType("BeetleThornBody");
            Length = 10;
            fadeSpeed = 6;
            partOffset = 34;
         
            

        }

        
    }

}