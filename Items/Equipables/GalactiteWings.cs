
using DRGN.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.Wings)]
    public class GalactiteWings : ModItem
    {


        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Unviverse traversing speed");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.value = 350000;
            item.rare = ItemRarities.GalacticRainbow;
            item.accessory = true;
        }
        //these wings use the same values as the solar wings
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 750;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 1.25f;
            ascentWhenRising = 0.65f;
            maxCanAscendMultiplier = 1.6f;
            maxAscentMultiplier = 3.8f;
            constantAscend = 0.4f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 13f;
            acceleration *= 3.5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidWings"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"),10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
