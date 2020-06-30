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
    public class Compressor : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Compressor");
            Tooltip.SetDefault("Turns low grade materials into higher grade ones");

        }
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 42;
            item.maxStack = 99;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = ItemRarityID.Green;
            item.value = 10000;
            item.consumable = true;
            item.createTile = mod.TileType("Compressor");
            item.autoReuse = true;

        }
        

    }
}
