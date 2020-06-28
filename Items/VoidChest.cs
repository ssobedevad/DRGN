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
    public class VoidChest : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Chest");
            Tooltip.SetDefault("Almost bottomless exept its the same size as normal");

        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = 6;
            item.consumable = true;
            item.createTile = mod.TileType("VoidChest");
            item.autoReuse = true;

        }
    }
}
