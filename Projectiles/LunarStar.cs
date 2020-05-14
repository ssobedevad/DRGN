using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.Projectiles
{
    public class LunarStar : ModProjectile
    {

        public override void SetDefaults()
        {

            projectile.height = 32;
            projectile.width = 32;
            projectile.aiStyle = -1;
            projectile.scale = 1f;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.light = 1f;
            projectile.penetrate = -1;
            projectile.alpha = 128;


        }

        public override void AI()
        {
            projectile.velocity.X *= 1.0001f;
            projectile.velocity.Y *= 1.001f;
            projectile.rotation += 0.4f;
           


            if (Main.rand.Next(2) == 0)
            {
                int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, Main.rand.Next(70, 75), projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 2f);
                Main.dust[DustID].noGravity = true;
            }


        }
        public override void Kill(int timeleft)
        {
            Item.NewItem(projectile.getRect(), mod.ItemType("LunarStar"));

        }

        }
}
