using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using DRGN.Rarities;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.Wings)]
    public class DragonWings : ModItem
    {
        

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Cut fresh");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = 200000;
            item.rare = ItemRarities.FieryOrange;
            item.accessory = true;
        }
        //these wings use the same values as the solar wings
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 400;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.95f;
            ascentWhenRising = 0.35f;
            maxCanAscendMultiplier = 1.3f;
            maxAscentMultiplier = 2.5f;
            constantAscend = 0.335f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 11f;
            acceleration *= 2.5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 20);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 20);
            recipe.AddIngredient(mod.ItemType("DragonFlyWings"));
            recipe.AddIngredient(mod.ItemType("GalacticEssence"), 20);
            recipe.AddIngredient(mod.ItemType("AntCrawlerScale"), 20);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
