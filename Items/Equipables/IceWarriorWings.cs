
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.Wings)]
    public class IceWarriorWings : ModItem
    {
        

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Stolen from an ancient being");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.value = 1000;
            item.rare = 10;
            item.accessory = true;
        }
        //these wings use the same values as the solar wings
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 250;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.55f;
            ascentWhenRising = 0.25f;
            maxCanAscendMultiplier = 0.7f;
            maxAscentMultiplier = 1.5f;
            constantAscend = 0.225f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 8f;
            acceleration *= 2f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlacialShard"), 60);
            recipe.AddIngredient(mod.ItemType("GlacialBar"), 60);
            recipe.AddIngredient(ItemID.FrozenWings);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
