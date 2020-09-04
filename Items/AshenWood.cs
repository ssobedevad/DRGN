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
    public class AshenWood : ModItem
    {

        public override void SetDefaults()
        {         
            item.maxStack = 999;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = ItemRarityID.Orange;
            item.value = 1000;
            item.consumable = true;
            item.createTile = mod.TileType("AshenWood");
            item.autoReuse = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AshenWoodWall"), 4);            
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(mod.ItemType("AshenPlatform"), 2);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
}
