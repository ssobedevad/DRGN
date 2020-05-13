
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GalactiteArmorRanged : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Galactite Visor");
            Tooltip.SetDefault("80% increased ranged damage" + "\n45% increased ranged crit" + "\n20% chance not to consume ammo");

        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 1000;
            item.rare = 12;
            item.defense = 45;

        }
        public override void UpdateEquip(Player player)
        {


            player.rangedDamage = (float)1.8 * player.rangedDamage;
            player.rangedCrit = (int)1.45 * player.rangedCrit;
            player.ammoCost80 = true;
            player.archery = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidVisor"));
            recipe.AddIngredient(mod.ItemType("LostIcewarriorRanged"));
            recipe.AddIngredient(mod.ItemType("FireDragonArmorRanged"));
            recipe.AddIngredient(mod.ItemType("ToxicArmorRanged"));
            recipe.AddIngredient(mod.ItemType("SnakeSkinArmorRanged"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
