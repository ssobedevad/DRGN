using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Items
{
    public class SnakeScale : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snake Scale");
            Tooltip.SetDefault("Scale of a snake");

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 27;
            item.maxStack = 99;
            item.rare = 4;
            item.value = 100;

        }

    }
}
