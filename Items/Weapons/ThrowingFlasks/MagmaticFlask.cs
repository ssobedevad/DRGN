
using DRGN.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.ThrowingFlasks
{
    public class MagmaticFlask : ThrowingFlask
    {
        public override void SafeSetDefaults()
        {
            item.damage = 85;
            item.useTime = 20;
            item.useAnimation = 20;
            item.value = 55000;
            item.rare = ItemRarities.FieryOrange;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("MagmaticFlask");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("MagmaticAntJaw"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}