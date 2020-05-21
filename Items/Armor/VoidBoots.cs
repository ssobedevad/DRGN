
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class VoidBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Void boots");
            Tooltip.SetDefault("1.3 base attack damage increase");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 12;
            item.value = 100000;
            item.rare = 12;
            item.defense = 40;

        }
        public override void UpdateEquip(Player player)
        {


            player.magicDamage  += 1.3f;
            player.rangedDamage += 1.3f;
            player.meleeDamage += 1.3f;
            player.thrownDamage += 1.3f;


        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(mod.ItemType("VoidBar"), 6);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 10);

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
