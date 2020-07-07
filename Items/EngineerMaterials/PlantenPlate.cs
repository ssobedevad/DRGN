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
    public class PlantenPlate : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Planten plate");
            Tooltip.SetDefault("Planten plate");

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.maxStack = 999;
            item.rare = ItemRarityID.Lime;
            item.value = 1000;

        }
        public override void AddRecipes()
        {


            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("IcyPlate"), 10);
            recipe.AddIngredient(mod.ItemType("PlantenConverter"));

            recipe.AddTile(mod.TileType("Compressor"));
            recipe.SetResult(this);
            recipe.AddRecipe(); 
        }

    }
}
