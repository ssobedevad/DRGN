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
    public class VoidStone : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Stone");
            Tooltip.SetDefault("No light can escape this");

        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = ItemRarityID.Purple;

            item.value = 1000;
            item.consumable = true;
            item.createTile = mod.TileType("VoidStoneTile");
            item.autoReuse = true;

        }
    }
}
