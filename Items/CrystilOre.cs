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
    public class CrystilOre : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = ItemRarityID.Purple;
            item.value = 1500;
            item.consumable = true;
            item.createTile = mod.TileType("CrystilOre");
            item.autoReuse = true;
        }
    }
}
