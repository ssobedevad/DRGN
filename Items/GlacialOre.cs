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
    public class GlacialOre : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glacial ore");
            Tooltip.SetDefault("brrr");

        }
        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = ItemRarityID.LightRed;
            item.value = 100;
            item.consumable = true;
            item.createTile = mod.TileType("GlacialOre");
            item.autoReuse = true;

        }
    }
}
