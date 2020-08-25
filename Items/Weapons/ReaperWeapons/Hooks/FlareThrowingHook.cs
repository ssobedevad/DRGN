using DRGN.Rarities;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.ReaperWeapons.Hooks
{
    public class FlareThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 112;
            item.useTime = 27;
            item.knockBack = 12f;
            item.value = 750000;
            item.rare = ItemRarities.FieryOrange;
            item.shoot = mod.ProjectileType("FlareHook");
            item.shootSpeed = 15f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 24);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 22);

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}