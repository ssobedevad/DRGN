
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

using DRGN.Rarities;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class FireDragonChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fire dragon chestplate");
            Tooltip.SetDefault("12% increased crit chance");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 180000;
            item.rare = ItemRarities.FieryOrange;
            item.defense = 44;

        }
       

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 12; 
            player.rangedCrit += 12; 
            player.meleeCrit += 12; 
            player.thrownCrit += 12;
            player.GetModPlayer<ReaperPlayer>().reaperCrit += 12;




        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 8);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 12);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
