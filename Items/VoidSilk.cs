using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using DRGN.Rarities;

namespace DRGN.Items
{
    public class VoidSilk : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void silk");
            Tooltip.SetDefault("Weaves the fabric of time and space");

        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;
            item.maxStack = 99;
            item.rare = ItemRarities.VoidPurple;

            item.value = 1000;
        }

    }
}
