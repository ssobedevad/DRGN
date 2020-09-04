

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class CrystilArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("7% increased crit chance"+"\n+1 crystils per explosion");
        }
        public override void SetDefaults()
        {
            item.value = 75000;
            item.rare = ItemRarityID.Purple;
            item.defense = 28;
        }
        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 7;
            player.rangedCrit += 7;
            player.meleeCrit += 7;
            player.thrownCrit += 7;
            player.GetModPlayer<ReaperPlayer>().reaperCrit += 7;
            player.GetModPlayer<DRGNPlayer>().crystalBoost += 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CrystilBar"), 20);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
