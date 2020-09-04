
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.ThrowingFlasks
{
    public class GlacialFlask : ThrowingFlask
    {
        public override void SafeSetDefaults()
        {
            item.damage = 25;
            item.useTime = 20;
            item.useAnimation = 20;
            item.value = 15000;
            item.rare = ItemRarityID.LightRed;
            item.shootSpeed = 9f;
            item.shoot = mod.ProjectileType("GlacialFlask");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlacialAntJaw"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}