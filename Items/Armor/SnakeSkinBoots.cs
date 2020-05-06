
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class SnakeSkinBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Snakeskin Boots");
            Tooltip.SetDefault("Doubled movement speed and acceleration");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 1000;
            item.rare = 2;
            item.defense = 3;
            
        }
        public override void UpdateEquip(Player player)
        {

            player.runAcceleration *= 2;
            player.maxRunSpeed *= 2;

        }
       

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SnakeScale"), 4);
            recipe.AddIngredient(ItemID.Cactus, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
