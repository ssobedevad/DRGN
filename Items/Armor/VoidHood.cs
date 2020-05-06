
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidHood : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Void Hood");
            Tooltip.SetDefault("120% increased Magic damage" + "\n35% decreased mana cost" + "\nGreatly increased mana regeneration" +"\n + 200 max mana");

        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 26;
            item.value = 1000;
            item.rare = 12;
            item.defense = 30;

        }
        public override void UpdateEquip(Player player)
        {


            player.magicDamage = (float)2.2 * player.magicDamage;
            player.manaCost = (float)0.65 * player.manaCost;
            player.manaRegen += 10;
            player.statManaMax2 += 200;

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
