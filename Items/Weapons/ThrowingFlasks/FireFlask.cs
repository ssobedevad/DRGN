
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.ThrowingFlasks
{
    public class FireFlask : ThrowingFlask
    {
        public override void SafeSetDefaults()
        {
            item.damage = 12;
            item.useTime = 20;
            item.useAnimation = 20;
            item.value = 9000;
            item.rare = ItemRarityID.Green;
            item.shootSpeed = 7f;
            item.shoot = mod.ProjectileType("FireFlask");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("FireAntJaw"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}