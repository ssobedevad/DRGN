using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Void Helmet");
            Tooltip.SetDefault("80% increased melee damage" + "\n40% increased melee crit" + "\n75% increased melee speed"+ "\n100 max health");

        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 1000;
            item.rare = 12;
            item.defense = 80;

        }
        public override void UpdateEquip(Player player)
        {


            player.meleeDamage = (float)1.8 * player.meleeDamage;
            player.meleeCrit = (int)1.4 * player.meleeCrit;
            player.meleeSpeed = (float)1.75 * player.meleeSpeed;
            player.statLifeMax2 += 100;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidSilk"), 6);
            recipe.AddIngredient(mod.ItemType("VoidBar"), 8);
            recipe.AddIngredient(mod.ItemType("VoidStone"), 18);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
