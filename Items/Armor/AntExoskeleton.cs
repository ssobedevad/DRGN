
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class AntExoskeleton : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased ant damage"+"\n5% increased ant speed");
        }

        public override void SetDefaults()
        {
            item.value = 6500;
            item.rare = ItemRarityID.Orange;
            item.defense = 6;

        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<DRGNPlayer>().antDamageMult += 0.1f;
            player.GetModPlayer<DRGNPlayer>().antSpeedMult += 0.05f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntEssence"), 14);
            recipe.AddIngredient(mod.ItemType("AntJaw"), 5);
            recipe.AddIngredient(mod.ItemType("FireAntJaw"), 5);
            recipe.AddIngredient(mod.ItemType("ToxicAntJaw"), 5);
            recipe.AddIngredient(mod.ItemType("ElectricAntJaw"), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
