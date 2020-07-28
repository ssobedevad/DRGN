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
    public class GalacticScale : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactic scale");
            Tooltip.SetDefault("Scale of the galaxy");

        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = ItemRarities.GalacticRainbow;
            item.value = 5000;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("ElementalJaw"));

            recipe.AddIngredient(mod.ItemType("AntCrawlerScale"));

            recipe.AddIngredient(mod.ItemType("AntEssence"));

            recipe.AddIngredient(mod.ItemType("AntWing"));
            recipe.AddIngredient(mod.ItemType("DragonFlyDust"));
            recipe.AddIngredient(mod.ItemType("SnakeScale"));
            recipe.AddIngredient(mod.ItemType("DragonScale"));
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"));
            recipe.AddIngredient(mod.ItemType("GlacialShard"));
            recipe.AddIngredient(mod.ItemType("LunarFragment"));

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }

    }
}
