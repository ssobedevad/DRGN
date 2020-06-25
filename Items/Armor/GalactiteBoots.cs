using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class GalactiteBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Galactite boots");
            Tooltip.SetDefault("1.5 attack damage increase");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 12;
            item.value = 550000;
            item.rare = ItemRarityID.Expert;
            item.defense = 55;

        }
        public override void UpdateEquip(Player player)
        {


            player.magicDamage += 1.5f;
            player.rangedDamage += 1.5f;
            player.meleeDamage += 1.5f;
            player.thrownDamage += 1.5f;


        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidBoots"));
            recipe.AddIngredient(mod.ItemType("CloudWarriorBoots"));
            recipe.AddIngredient(mod.ItemType("LostIcewarriorBoots"));
            recipe.AddIngredient(mod.ItemType("FireDragonBoots"));
            recipe.AddIngredient(mod.ItemType("ToxicArmorBoots"));
            recipe.AddIngredient(mod.ItemType("SnakeSkinBoots"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 12);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
