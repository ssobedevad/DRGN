using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Projectiles.Obsidian
{
    public class ObsidianThrowingAxe : ModProjectile
    {





        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;





        }
        public override void AI()
        {





            projectile.rotation += 0.35f;
            projectile.velocity.Y += 0.05f;




        }
        public override void Kill(int timeleft)
        {
            if (Main.rand.NextBool())
            {
                Item.NewItem(projectile.Hitbox, mod.ItemType("ObsidianThrowingAxe"));
            }
        }


    }
}