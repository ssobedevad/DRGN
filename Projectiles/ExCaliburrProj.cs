using Terraria;
using System;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
namespace DRGN.Projectiles
{
    public class ExCaliburrProj : RiochetProjectile
    {

       
        public override void SafeSetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;
            
            projectile.melee = true;
            projectile.penetrate = 5;
            Gravity = 0.2f;
            TerminalVelocity = 16f;
            RiochetSpeed = 16f;
           
        }
        



    }
}
