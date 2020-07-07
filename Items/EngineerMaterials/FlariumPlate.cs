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
    public class FlariumPlate : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flarium plate");
            Tooltip.SetDefault("Flarium plate");

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.maxStack = 999;
            item.rare = ItemRarityID.Red;
            item.value = 10000;

        }
        public override void AddRecipes()
        {


            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("InsectiumPlate"), 10);
            recipe.AddIngredient(mod.ItemType("FlariumConverter"));

            recipe.AddTile(mod.TileType("Compressor"));
            recipe.SetResult(this);
             recipe.AddRecipe(); 
        }

    }
}
