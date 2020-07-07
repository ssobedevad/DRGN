using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.Wings)]
    public class DragonFlyWings : ModItem
    {
        

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("They can carry the biggest dragonfly but im not too sure about a person");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = 100000;
            item.rare = ItemRarityID.Red;
            item.accessory = true;
        }
        //these wings use the same values as the solar wings
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 375;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.32f;
            maxCanAscendMultiplier = 1.2f;
            maxAscentMultiplier = 2.2f;
            constantAscend = 0.305f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 10f;
            acceleration *= 2.2f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"), 20);
            recipe.AddIngredient(mod.ItemType("DragonFlyWing"), 20);
            recipe.AddRecipeGroup("DRGN:T12Wings");
            recipe.AddIngredient(mod.ItemType("IceWarriorWings"));
            recipe.AddIngredient(mod.ItemType("DragonFlyDust"), 20);
            
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
