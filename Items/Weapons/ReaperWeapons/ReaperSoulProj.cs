using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Graphics;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Weapons.ReaperWeapons
{
    public class ReaperSoulProj : ModProjectile
    {
        
        public override string Texture => "DRGN/Items/Weapons/ReaperWeapons/ReaperSoul";
        public override void SetDefaults()
        {

            projectile.timeLeft = 60;
            
            Main.projFrames[projectile.type] = 4;
            projectile.aiStyle = -1;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.height = 28;
            projectile.width = 22;
        }
        public override void AI()
        {
            projectile.velocity *= 0.95f;
            if (projectile.frameCounter > 5)
            {
                projectile.frame += 1;
                if (projectile.frame == 4) { projectile.frame = 0; }
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
