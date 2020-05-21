
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Void Mask");
            Tooltip.SetDefault("80% increased throwing velocity"+"\n65% increased throwing damage");

        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 1000;
            item.rare = 12;
            item.defense = 40;

        }
        public override void UpdateEquip(Player player)
        {


            player.thrownVelocity = (float)1.8 * player.thrownVelocity;
            player.thrownDamage = (float)1.65 * player.thrownDamage;


        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(mod.ItemType("VoidBar"), 8);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 18);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
