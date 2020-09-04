
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class CrystilBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("160% increased run acceleration and 65% increased wing time" + "\n10% decreased crystal spread");
        }
        public override void SetDefaults()
        {
            item.value = 65000;
            item.rare = ItemRarityID.Purple;
            item.defense = 20;
        }
        public override void UpdateEquip(Player player)
        {
            player.runAcceleration *= 2.6f;
            player.maxRunSpeed *= 2.6f;
            player.wingTimeMax = (int)(1.65f * player.wingTimeMax);
            player.GetModPlayer<DRGNPlayer>().crystalAccuracy *= 0.9f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CrystilBar"), 18);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
