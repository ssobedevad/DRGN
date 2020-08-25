using DRGN.Rarities;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.ReaperWeapons.Hooks
{
    public class GalacticThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 195;
            item.useTime = 22;
            item.knockBack = 9f;
            item.value = 1200000;
            item.rare = ItemRarities.GalacticRainbow;
            item.shoot = mod.ProjectileType("GalacticHook");
            item.shootSpeed = 20f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidThrowingHook"));
            recipe.AddIngredient(mod.ItemType("FlareThrowingHook"));
            recipe.AddIngredient(mod.ItemType("DragonFlyThrowingHook"));
            recipe.AddIngredient(mod.ItemType("LunarThrowingHook"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 12);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}