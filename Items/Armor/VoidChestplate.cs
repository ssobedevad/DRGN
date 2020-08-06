
using DRGN.Rarities;
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
            Tooltip.SetDefault("200% movement speed and acceleration increase and double wing time");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 18;
            item.value = 580000;
            item.rare = ItemRarities.VoidPurple;
            item.defense = 40;

        }
       

        public override void UpdateEquip(Player player)
        {


            player.runAcceleration *= 3f;
            player.maxRunSpeed *= 3f;
            player.wingTimeMax = (int)(2f * player.wingTimeMax);
            player.enemySpawns = true;

        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(mod.ItemType("VoidBar"), 8);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 20);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
