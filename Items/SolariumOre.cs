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
    public class SolariumOre : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solarium ore");
            Tooltip.SetDefault("Its rather hot and thats it really");

        }
        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = ItemRarityID.Red;
            item.value = 1000;
            item.consumable = true;
            item.createTile = mod.TileType("SolariumOre");
            item.autoReuse = true;

        }
    }
}
