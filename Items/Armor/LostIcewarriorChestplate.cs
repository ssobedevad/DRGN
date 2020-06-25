
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class LostIcewarriorChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Lost Ice Warrior chestplate");
            Tooltip.SetDefault("Grants various useful utilities");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 21000;
            item.rare = ItemRarityID.LightRed;
            item.defense = 25;

        }
        
        public override void UpdateEquip(Player player)
        {


            player.accWatch = 3;
          
            player.enemySpawns = true;
            player.blockRange += 25;
            player.nightVision = true;
            player.noFallDmg = true;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlacialShard"), 8);
            recipe.AddIngredient(mod.ItemType("GlacialBar"), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
