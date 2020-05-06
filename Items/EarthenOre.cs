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
    public class EarthenOre : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earthen ore");
            Tooltip.SetDefault("Feels alive");

        }
        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = 6;
            item.consumable = true;
            item.createTile = mod.TileType("EarthenOre");
            item.autoReuse = true;

        }
    }
}
