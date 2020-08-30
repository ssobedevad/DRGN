
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ShadeChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Grants +2 defense and + 2% reaper damage per rotating scythe alive");
        }

        public override void SetDefaults()
        {

            item.value = 8000;
            item.rare = ItemRarityID.Orange;
            item.defense = 4;

        }

        public override void UpdateEquip(Player player)
        {
            int numScythes = player.GetModPlayer<ReaperPlayer>().rotatingScythes;
            player.statDefense += numScythes * 2;
            player.GetModPlayer<ReaperPlayer>().reaperDamageMult += (0.02f * numScythes);

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShadowDemonChestplate"));
            recipe.AddIngredient(mod.ItemType("ShadeCrystal"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
