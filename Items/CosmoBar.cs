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
    public class CosmoBar : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmo Bar");
            Tooltip.SetDefault("Day and night fused into one");

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
            item.rare = 13;
            item.value = 100000;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CosmoOre"), 3);
            recipe.AddTile(mod.TileType("IndustrialForgeTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
