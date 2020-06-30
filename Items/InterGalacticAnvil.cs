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
    public class InterGalacticAnvil : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Intergalactic anvil");
            Tooltip.SetDefault("The Anvil of Gods");

        }
        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 28;
            item.maxStack = 99;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = ItemRarityID.Yellow;
            item.value = 10000;
            item.consumable = true;
            item.createTile = mod.TileType("InterGalacticAnvilTile");
            item.autoReuse = true;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 30);
            recipe.AddRecipeGroup("DRGN:HmAnvil");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
