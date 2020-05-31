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
    public class VoidPlate : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void plate");
            Tooltip.SetDefault("Void plate");

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
            recipe.AddIngredient(mod.ItemType("FlariumPlate"), 10);
            recipe.AddIngredient(mod.ItemType("VoidConverter"));

            recipe.AddTile(mod.TileType("Compressor"));
            recipe.SetResult(this);
            recipe.AddRecipe(); 
        }

    }
}
