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
    public class InsectiumScrew : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Insectium Screw");
            Tooltip.SetDefault("Insectium Screw");

        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.maxStack = 999;
            item.rare = ItemRarityID.Red;
            item.value = 5000;

        }
        public override void AddRecipes()
        {


            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LunarScrew"), 10);
            recipe.AddIngredient(mod.ItemType("InsectiumConverter"));

            recipe.AddTile(mod.TileType("Compressor"));
            recipe.SetResult(this);
            recipe.AddRecipe(); 
        }

    }
}
