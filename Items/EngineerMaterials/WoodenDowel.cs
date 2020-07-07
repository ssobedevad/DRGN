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
    public class WoodenDowel : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wooden dowel");
            Tooltip.SetDefault("Standard wooden dowel");

        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.maxStack = 999;
            item.rare = ItemRarityID.Blue;
            item.value = 50;

        }

    }
}
