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
    public class DragonFlyWing : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon fly wing");
            Tooltip.SetDefault("A wing from a dragon fly");

        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 19;
            item.maxStack = 99;
            item.rare = 12;
            item.value = 10000;
        }

    }
}
