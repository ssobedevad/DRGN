
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ToxicArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Toxic Breastplate");
            Tooltip.SetDefault("Greatly increased life regen");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 20;
            item.value = 2000;
            item.rare =ItemRarityID.Green;
            item.defense = 6;
            
        }
       
        public override void UpdateEquip(Player player)
        {
             
            player.lifeRegen += 3;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"), 25);
            recipe.AddIngredient(mod.ItemType("EarthenBar"), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
