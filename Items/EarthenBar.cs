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
    public class EarthenBar : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earthen Bar");
            Tooltip.SetDefault("Sticky and alive");

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
            item.rare = ItemRarityID.Green;
            item.value = 1000;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("BarPile");
            item.placeStyle = 1;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("EarthenOre"), 12);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}
