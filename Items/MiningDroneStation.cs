using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using DRGN.Tiles;

namespace DRGN.Items
{
    public class MiningDroneStation : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mining Drone Station");
            Tooltip.SetDefault("Only one may be in a world at a time");

        }
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 42;
            item.maxStack = 99;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Yellow;
            item.value = 10000;
            item.consumable = true;
            item.createTile = mod.TileType("MiningDroneStation");
            item.autoReuse = true;

        }

        public override bool CanUseItem(Player player)
        {
            
            if(DRGNModWorld.MiningDroneStation)
            { return false; }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddRecipeGroup("IronBar", 50);
            recipe.AddIngredient(ItemID.IllegalGunParts, 2);
            recipe.AddIngredient(ItemID.Bomb, 5);
            recipe.AddIngredient(ItemID.Dynamite, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
