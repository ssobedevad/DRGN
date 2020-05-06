using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class VoidPick : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void pickaxe");
            Tooltip.SetDefault("Could rip holes in time and space");
        }

        public override void SetDefaults()
        {
            item.damage = 150;

            item.melee = true;

            item.useTurn = true;
            item.rare = 18;
            item.width = 32;
            item.height = 32;
            item.useTime = 1;
            item.autoReuse = true;
            item.useStyle = 1;
            item.shootSpeed = 14f;
            item.useAnimation = 15;
            item.pick = 395;
            item.axe = 300;
            item.value = 1000000;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidSilk"), 12);
            recipe.AddIngredient(mod.ItemType("VoidBar"), 25);
            recipe.AddIngredient(mod.ItemType("VoidStone"), 35);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
