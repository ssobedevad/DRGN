using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Items.EngineerMaterials
{
    public class GoldenPlate : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Golden plate");
            Tooltip.SetDefault("Golden plate");

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.maxStack = 999;
            item.rare = 4;
            item.value = 100;

        }
        public override void AddRecipes()
        {


            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("MetalPlate"), 10);
            recipe.AddIngredient(mod.ItemType("GoldenConverter"));

            recipe.AddTile(mod.TileType("Compressor"));
            recipe.SetResult(this);
             recipe.AddRecipe(); 
        }

    }
}
