
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class AshenChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Greatly increased damage and defense while in hell");
        }

        public override void SetDefaults()
        {
            item.value = 5000;
            item.rare = ItemRarityID.Orange;
            item.defense = 7;

        }

        public override void UpdateEquip(Player player)
        {
            if(player.ZoneUnderworldHeight)
            { 
                player.statDefense += 10;
                player.allDamageMult += 0.1f;
            }

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 14);
            recipe.AddIngredient(mod.ItemType("AshenWood"), 14);
            recipe.AddIngredient(mod.ItemType("FlareCrystal"), 14);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
