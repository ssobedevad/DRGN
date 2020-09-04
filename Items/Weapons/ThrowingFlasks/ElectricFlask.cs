
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.ThrowingFlasks
{
    public class ElectricFlask : ThrowingFlask
    {
        public override void SafeSetDefaults()
        {
            item.damage = 8;
            item.useTime = 20;
            item.useAnimation = 20;
            item.value = 9000;
            item.rare = ItemRarityID.Green;
            item.shootSpeed = 6.5f;
            item.shoot = mod.ProjectileType("ElectricFlask");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ElectricAntJaw"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}