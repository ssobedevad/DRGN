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
    public class PlantenScrew : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Planten Screw");
            Tooltip.SetDefault("Planten Screw");

        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.maxStack = 999;
            item.rare = 4;
            item.value = 100;

        }
        public override void AddRecipes()
        {


            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("IcyScrew"), 10);
            recipe.AddIngredient(mod.ItemType("PlantenConverter"));

            recipe.AddTile(mod.TileType("Compressor"));
            recipe.SetResult(this);
            recipe.AddRecipe(); 
        }

    }
}
