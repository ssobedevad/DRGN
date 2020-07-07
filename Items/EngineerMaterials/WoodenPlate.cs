using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Items.EngineerMaterials
{
    public class WoodenPlate : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wooden plate");
            Tooltip.SetDefault("Standard wooden plate");

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.maxStack = 999;
            item.rare = ItemRarityID.Blue;
            item.value = 50;

        }

    }
}
