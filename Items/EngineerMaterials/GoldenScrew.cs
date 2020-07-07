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
    public class GoldenScrew : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("GoldenScrew");
            Tooltip.SetDefault("Golden screw");

        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.maxStack = 999;
            item.rare = ItemRarityID.Orange;
            item.value = 250;

        }
        public override void AddRecipes()
        {


            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Screw"), 10);
            recipe.AddIngredient(mod.ItemType("GoldenConverter"));

            recipe.AddTile(mod.TileType("Compressor"));
            recipe.SetResult(this);
            recipe.AddRecipe(); 
        }

    }
}
