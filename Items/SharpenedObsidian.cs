using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Items
{
    public class SharpenedObsidian : ModItem
    {


        public override void SetDefaults()
        {

            item.maxStack = 99;
            item.rare = ItemRarityID.Orange;
            item.value = 300;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            

            recipe.AddIngredient(ItemID.Obsidian);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}
