
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.Wings)]
    public class VoidWings : ModItem
    {
        

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Warp Speed");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.value = 10000;
            item.rare = 2;
            item.accessory = true;
        }
        //these wings use the same values as the solar wings
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 550;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 1.05f;
            ascentWhenRising = 0.45f;
            maxCanAscendMultiplier = 1.4f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.355f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 13f;
            acceleration *= 3.5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidSilk"), 60);
            recipe.AddIngredient(mod.ItemType("VoidBar"), 60);
            recipe.AddIngredient(mod.ItemType("VoidBrick"), 60);
            recipe.AddIngredient(mod.ItemType("VoidStone"), 60);
            recipe.AddIngredient(mod.ItemType("DragonWings"));
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
