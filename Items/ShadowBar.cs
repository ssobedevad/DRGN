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
    public class ShadowBar : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Bar");           
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
            item.rare = ItemRarityID.Blue;
            item.value = 800;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("BarPile");
            item.placeStyle = 8;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneOre,2);
            recipe.AddIngredient(ItemID.Vertebrae);
            recipe.AddIngredient(ItemID.Shadewood,2);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.DemoniteOre,2);
            recipe2.AddIngredient(ItemID.RottenChunk);
            recipe2.AddIngredient(ItemID.Ebonwood, 2);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
}
