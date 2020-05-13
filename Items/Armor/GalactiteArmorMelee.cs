using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GalactiteArmorMelee : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Galactite Helmet");
            Tooltip.SetDefault("75% increased melee damage" + "\n40% increased melee crit" + "\n66% increased melee speed" + "\n150 max health");

        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 1000;
            item.rare = 12;
            item.defense = 95;

        }
        public override void UpdateEquip(Player player)
        {


            player.meleeDamage = (float)1.75 * player.meleeDamage;
            player.meleeCrit = (int)1.4 * player.meleeCrit;
            player.meleeSpeed = (float)1.66 * player.meleeSpeed;
            player.statLifeMax2 += 150;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidHelmet"));
            recipe.AddIngredient(mod.ItemType("LostIcewarriorMelee"));
            recipe.AddIngredient(mod.ItemType("FireDragonHelm"));
            recipe.AddIngredient(mod.ItemType("ToxicArmorMelee"));
            recipe.AddIngredient(mod.ItemType("SnakeSkinArmorMelee"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
