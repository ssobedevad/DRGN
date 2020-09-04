
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons.ThrowingFlasks
{
    public class AntFlask : ThrowingFlask
    {
        public override void SafeSetDefaults()
        {
            item.damage = 10;
            item.useTime = 20;
            item.useAnimation = 20;
            item.value = 10000;
            item.rare = ItemRarityID.Orange;
            item.shootSpeed = 8f;
            item.shoot = mod.ProjectileType("AntFlask");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntJaw"));            
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this,5);
            recipe.AddRecipe();
        }
    }
}