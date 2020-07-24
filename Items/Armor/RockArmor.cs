
using DRGN.Items.EngineerClass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class RockArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Rock chestplate");
            Tooltip.SetDefault("25% increased flail and yoyo crit chance");
        }

        public override void SetDefaults()
        {
            
            item.value = 40000;
            item.rare = ItemRarityID.Lime;
            item.defense = 28;

        }


        public override void UpdateEquip(Player player)
        {
         

            player.GetModPlayer<DRGNPlayer>().YoyoBonusCrit += 25;
            player.GetModPlayer<DRGNPlayer>().FlailBonusCrit += 25;




        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(mod.ItemType("LihzahrdBar"), 12);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
