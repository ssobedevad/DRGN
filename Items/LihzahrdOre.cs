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
    public class LihzahrdOre : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lihzahrd Ore");
            Tooltip.SetDefault("Bits of green men in rock");

        }
        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = ItemRarityID.Lime;
            item.value = 900;
            item.consumable = true;
            item.createTile = mod.TileType("LihzahrdOre");
            item.autoReuse = true;

        }
    }
}
