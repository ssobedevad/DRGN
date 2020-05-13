
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class VoidChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Void chestplate");
            Tooltip.SetDefault("275% movement speed and acceleration increase and double wing time");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 18;
            item.value = 1000;
            item.rare = 12;
            item.defense = 55;

        }
       

        public override void UpdateEquip(Player player)
        {


            player.runAcceleration = (float)(player.runAcceleration * 2.75f);
            player.maxRunSpeed = (float)(player.maxRunSpeed * 2.75f);
            player.wingTimeMax = (int)(2f * player.wingTimeMax);
            player.enemySpawns = true;

        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidSilk"), 8);
            recipe.AddIngredient(mod.ItemType("VoidBar"), 8);
            recipe.AddIngredient(mod.ItemType("VoidStone"), 20);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
