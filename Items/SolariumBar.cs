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
    public class SolariumBar : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solarium bar");
            Tooltip.SetDefault("pretty tough and kinda hot");

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
            item.rare = 13;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SolariumOre"), 12);
            recipe.AddTile(mod.TileType("IndustrialForgeTile"));
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}
