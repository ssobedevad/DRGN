using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class GalactiteArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Galactite chestplate");
            Tooltip.SetDefault("325% movement speed and acceleration increase and 2.5 times wing time");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 18;
            item.value = 1000;
            item.rare = 12;
            item.defense = 80;

        }


        public override void UpdateEquip(Player player)
        {


            player.runAcceleration = (float)(player.runAcceleration * 3.25f);
            player.maxRunSpeed = (float)(player.maxRunSpeed * 3.25f);
            player.wingTimeMax = (int)(2.5f * player.wingTimeMax);
            player.enemySpawns = true;

        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidChestplate"));
            recipe.AddIngredient(mod.ItemType("LostIcewarriorChestplate"));
            recipe.AddIngredient(mod.ItemType("FireDragonChestplate"));
            recipe.AddIngredient(mod.ItemType("ToxicArmor"));
            recipe.AddIngredient(mod.ItemType("SnakeSkinBreastplate"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 20);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
