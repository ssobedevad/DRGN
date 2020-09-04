
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.ThrowingFlasks
{
    public class ToxicFlask : ThrowingFlask
    {
        public override void SafeSetDefaults()
        {
            item.damage = 10;
            item.useTime = 20;
            item.useAnimation = 20;
            item.value = 8000;
            item.rare = ItemRarityID.Green;
            item.shootSpeed = 6f;
            item.shoot = mod.ProjectileType("ToxicFlask");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ToxicAntJaw"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}