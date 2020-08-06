

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class CloudWarriorArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Cloud Warrior chestplate");
            Tooltip.SetDefault("5% increased crit chance");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 60000;
            item.rare = ItemRarityID.Yellow;
            item.defense = 26;

        }


        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 5;
            player.rangedCrit += 5;
            player.meleeCrit += 5;
            player.thrownCrit += 5;


            player.GetModPlayer<ReaperPlayer>().reaperCrit += 5;




        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(mod.ItemType("CosmoBar"), 12);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
