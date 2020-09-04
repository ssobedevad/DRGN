
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class AntBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+2 max ants" + "\n10% increased ant speed");
        }

        public override void SetDefaults()
        {
            item.value = 8500;
            item.rare = ItemRarityID.Orange;
            item.defense = 4;

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<DRGNPlayer>().maxAnts += 2;
            player.GetModPlayer<DRGNPlayer>().antSpeedMult += 0.1f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntEssence"), 10);
            recipe.AddIngredient(mod.ItemType("AntJaw"), 4);
            recipe.AddIngredient(mod.ItemType("FireAntJaw"), 4);
            recipe.AddIngredient(mod.ItemType("ToxicAntJaw"), 4);
            recipe.AddIngredient(mod.ItemType("ElectricAntJaw"), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
