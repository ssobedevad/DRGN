using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DRGN.Items
{
    public class LunarStar : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Star");
            Tooltip.SetDefault("Straight from the heavens");

        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.rare = ItemRarityID.Red;
            item.value = 10000;
        }
        public override void PostUpdate()
        {
            if (Main.rand.Next(2) == 0)
            {
                int DustID = Dust.NewDust(new Vector2(item.position.X, item.position.Y + 2f), item.width + 4, item.height + 4, Main.rand.Next(70, 75), 0, 0, 120, default(Color), 2f);
                Main.dust[DustID].noGravity = true;
            }
            if (Main.dayTime) { item.active = false; }
        }

    }
}
