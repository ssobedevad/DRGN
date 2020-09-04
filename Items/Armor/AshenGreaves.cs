
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class AshenGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grants the ability to walk on lava and grants lava immunity"+"\n10% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.value = 6500;
            item.rare = ItemRarityID.Orange;
            item.defense = 5;

        }
        public override void UpdateEquip(Player player)
        {
            player.lavaImmune = true;
            player.waterWalk = true;
            player.waterWalk2 = true;
            player.runAcceleration *= 1.1f;
            player.maxRunSpeed *= 1.1f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(mod.ItemType("AshenWood"), 10);
            recipe.AddIngredient(mod.ItemType("FlareCrystal"), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
