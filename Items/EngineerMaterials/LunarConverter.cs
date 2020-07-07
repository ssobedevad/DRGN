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
    public class LunarConverter : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Converter");
            Tooltip.SetDefault("Used to turn planten materials into lunar ones");

        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = ItemRarityID.Cyan;
            item.value = 2500;

        }
        

    }
}
