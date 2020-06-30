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
    public class DragonBrick : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Brick");
            Tooltip.SetDefault("Made of decomposed dragon");

        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = ItemRarityID.Red;
            item.value = 1000;
            item.consumable = true;
            item.createTile = mod.TileType("DragonBrick");
            item.autoReuse = true;

        }
    }
}
