
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidVisor : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Void Visor");
            Tooltip.SetDefault("60% increased ranged damage" + "\n35% increased ranged crit" + "\n20% chance not to consume ammo");

        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 1000;
            item.rare = 12;
            item.defense = 30;

        }
        public override void UpdateEquip(Player player)
        {


            player.rangedDamage = (float)1.6 * player.rangedDamage;
            player.rangedCrit = (int)1.35 * player.rangedCrit;
            player.ammoCost80 = true;
            player.archery = true;
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
