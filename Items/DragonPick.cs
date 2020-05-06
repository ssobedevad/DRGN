using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items
{
    public class DragonPick : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon pickaxe");
            Tooltip.SetDefault("Mines quick and stays together");
        }

        public override void SetDefaults()
        {
            item.damage = 100;
            
            item.melee = true;

            item.useTurn = true;
            item.rare = 15;
            item.width = 35;
            item.height = 35;
            item.useTime = 1;
            item.autoReuse = true;
            item.useStyle = 1;
            item.shootSpeed = 14f;
            item.useAnimation = 15;
            item.pick = 300;
            item.axe = 300;
            item.value = 10000;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 12);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 25);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
