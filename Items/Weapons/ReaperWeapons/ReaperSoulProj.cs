using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.ReaperWeapons
{
    public class ReaperSoulProj : ModProjectile
    {             
        public override void SetDefaults()
        {
            projectile.timeLeft = 60;           
            Main.projFrames[projectile.type] = 4;
            projectile.aiStyle = 0;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.height = 22;
            projectile.width = 22;
        }
        public override void AI()
        {
            projectile.velocity *= 0.95f;
            if (projectile.frameCounter > 5)
            {
                projectile.frame += 1;
                if (projectile.frame >= 4) { projectile.frame = 0; }
                projectile.frameCounter = 0;
            }
            projectile.frameCounter += 1;
        }
        public override void Kill(int timeLeft)
        {
            int item = Item.NewItem(projectile.getRect(), mod.ItemType("ReaperSoul"),1,false,0,true);
            Main.item[item].velocity = Vector2.Zero;           
        }
    }
}
