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
    public class MetalPlate : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metal plate");
            Tooltip.SetDefault("Standard metal plate");

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.maxStack = 999;
            item.rare = ItemRarityID.Green;
            item.value = 100;

        }
        public override void AddRecipes()
        {


            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("WoodenPlate"), 10);
            recipe.AddIngredient(mod.ItemType("MetalloidConverter"));

            recipe.AddTile(mod.TileType("Compressor"));
            recipe.SetResult(this);
             recipe.AddRecipe(); 
        }

    }
}
