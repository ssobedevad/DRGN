
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class SnakeSkinArmorRanged : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Snakeskin Visor");
            Tooltip.SetDefault("15% increased ranged damage." + "\n20% chance not to consume ammo");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 1000;
            item.rare = 2;
            item.defense = 4;

        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage = (float)(1.15 * player.rangedDamage);
            player.ammoCost80 =true;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SnakeScale"), 8);
            recipe.AddIngredient(ItemID.Cactus, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }



}
