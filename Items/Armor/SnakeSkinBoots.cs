
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
            Tooltip.SetDefault("25% increased speed and acceleration");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 800;
            item.rare = ItemRarityID.Blue;
            item.defense = 4;
            
        }
        public override void UpdateEquip(Player player)
        {

            player.runAcceleration *= 1.25f;
            player.maxRunSpeed *= 1.25f;

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
