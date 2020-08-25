using DRGN.Rarities;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.ReaperWeapons.Hooks
{
    public class VoidThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 130;
            item.useTime = 24;
            item.knockBack = 9f;
            item.value = 950000;
            item.rare = ItemRarities.VoidPurple;
            item.shoot = mod.ProjectileType("VoidHook");
            item.shootSpeed = 17.5f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 22);

            recipe.AddIngredient(mod.ItemType("VoidBar"), 25);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}