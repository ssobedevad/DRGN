using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using DRGN.Rarities;

namespace DRGN.Items
{
    public class GalacticaBar : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactica Bar");
            Tooltip.SetDefault("Holds the power of the universe");

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
            item.rare = ItemRarities.GalacticRainbow;

            item.value = 100000;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("BarPile");
            item.placeStyle = 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GalacticaOre"), 20);
            recipe.AddIngredient(mod.ItemType("TechnoBar"));
            recipe.AddIngredient(mod.ItemType("LunarFragment"));
            recipe.AddIngredient(mod.ItemType("EarthenBar"));
            
            
            recipe.AddIngredient(mod.ItemType("SolariumBar"));
            
            
            recipe.AddIngredient(mod.ItemType("GlacialBar"));

            recipe.AddIngredient(mod.ItemType("CosmoBar"));
            recipe.AddIngredient(ItemID.LunarBar);
            recipe.AddIngredient(mod.ItemType("LunarStar"));

            recipe.AddIngredient(mod.ItemType("VoidBar"));
            recipe.AddIngredient(mod.ItemType("VoidSoul"));

            recipe.AddTile(mod.TileType("IndustrialForgeTile"));
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }
    }
}
