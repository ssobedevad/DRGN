using Terraria.ID;
using Terraria.ModLoader;
namespace DRGN.Items.Weapons.ReaperWeapons.Scythes
{
    public class AntSlasher : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 25;
            item.knockBack = 6f;
            item.value = 50000;
            item.rare = ItemRarityID.Orange;
            item.useTime = 30;
            item.shootSpeed = 7.75f;
            item.shoot = mod.ProjectileType("AntSlasher");
            shoot2 = mod.ProjectileType("AntSlasherThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntJaw"), 14);
            recipe.AddIngredient(mod.ItemType("AntEssence"), 14);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}