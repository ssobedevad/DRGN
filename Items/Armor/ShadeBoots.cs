
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class ShadeBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("12% increased movement speed and acceleration" + "\n+8 Reaper Crit");
        }

        public override void SetDefaults()
        {
            item.value = 1400;
            item.rare = ItemRarityID.Orange;
            item.defense = 4;

        }
        public override void UpdateEquip(Player player)
        {

            player.runAcceleration *= 1.12f;
            player.maxRunSpeed *= 1.12f;
            player.GetModPlayer<ReaperPlayer>().reaperCrit += 8;

        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShadowDemonBoots"));
            recipe.AddIngredient(mod.ItemType("ShadeCrystal"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
