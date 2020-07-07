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
    public class IcyPipe : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icy Pipe");
            Tooltip.SetDefault("Icy pipe");

        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.maxStack = 999;
            item.rare = ItemRarityID.LightRed;
            item.value = 500;

        }
        public override void AddRecipes()
        {


            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GoldenPipe"), 10);
            recipe.AddIngredient(mod.ItemType("IcyConverter"));

            recipe.AddTile(mod.TileType("Compressor"));
            recipe.SetResult(this);
            recipe.AddRecipe(); 
        }

    }
}
