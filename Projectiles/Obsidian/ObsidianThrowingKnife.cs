using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles.Obsidian
{
    public class ObsidianThrowingKnife : ModProjectile
    {





        public override void SetDefaults()
        {

            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 2;
            projectile.height = 12;
            projectile.width = 12;




        }
        public override void AI()
        {





            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            projectile.velocity.Y += 0.05f;




        }



    }
}