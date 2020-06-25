
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class FireDragonBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fire dragon boots");
            Tooltip.SetDefault("300% increased acceleration and 80% increased wingtime");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 130000;
            item.rare = ItemRarityID.Red;
            item.defense = 35;

        }
        public override void UpdateEquip(Player player)
        {


            player.runAcceleration *= 3;
            player.maxRunSpeed *= 3;
            player.wingTimeMax = (int)(1.8f * player.wingTimeMax);



        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 4);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 6);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
