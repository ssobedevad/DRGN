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
    public class IndustrialForge : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Industrial Forge");
            Tooltip.SetDefault("Good for mass production");

        }
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 42;
            item.maxStack = 99;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = 6;
            item.consumable = true;
            item.createTile = mod.TileType("IndustrialForgeTile");
            item.autoReuse = true;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 30);
            recipe.AddRecipeGroup("DRGN:T3Forge");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}
