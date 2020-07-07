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
    public class VoidBar : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void bar");
            Tooltip.SetDefault("Carries the missing matter and energy from the universe");

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
            item.rare = ItemRarityID.Purple;
            
            item.value = 100000;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("BarPile");
            item.placeStyle = 5;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidOre"), 4);
            recipe.AddTile(mod.TileType("IndustrialForgeTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
