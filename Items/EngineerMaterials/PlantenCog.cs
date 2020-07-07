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
    public class PlantenCog : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Planten Cog");
            Tooltip.SetDefault("Planten cog");

        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.maxStack = 999;
            item.rare = ItemRarityID.Lime;
            item.value = 1000;

        }
        public override void AddRecipes()
        {


            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("IcyCog"), 10);
            recipe.AddIngredient(mod.ItemType("PlantenConverter"));

            recipe.AddTile(mod.TileType("Compressor"));
            recipe.SetResult(this);
             recipe.AddRecipe(); 
        }

    }
}
