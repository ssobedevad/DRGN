
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.ThrowingFlasks
{
    public class LunarFlask : ThrowingFlask
    {
        public override void SafeSetDefaults()
        {
            item.damage = 55;
            item.useTime = 20;
            item.useAnimation = 20;
            item.value = 35000;
            item.rare = ItemRarityID.Red;
            item.shootSpeed = 13f;
            item.shoot = mod.ProjectileType("LunarFlask");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LunarAntJaw"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}