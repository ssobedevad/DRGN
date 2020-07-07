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
    public class LunarScrew : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Screw");
            Tooltip.SetDefault("Lunar Screw");

        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.maxStack = 999;
            item.rare = ItemRarityID.Cyan;
            item.value = 2500;

        }
        public override void AddRecipes()
        {


            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("PlantenScrew"), 10);
            recipe.AddIngredient(mod.ItemType("LunarConverter"));

            recipe.AddTile(mod.TileType("Compressor"));
            recipe.SetResult(this);
            recipe.AddRecipe(); 
        }

    }
}
