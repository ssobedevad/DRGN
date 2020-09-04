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
    public class VoidOre : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void ore");
            Tooltip.SetDefault("Has a certain feel of power");

        }
        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("VoidOre");
            item.autoReuse = true;
            item.rare = ItemRarities.VoidPurple;

            item.value = 1000;

        }
    }
}
