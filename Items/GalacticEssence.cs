using DRGN.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
namespace DRGN.Items
{
    public class GalacticEssence : ModItem
    {



        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactic Essence");
            Tooltip.SetDefault("A fragment of the universe");
            ItemID.Sets.ItemNoGravity[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));


        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = ItemRarities.GalacticRainbow;
            item.value = 5000;
            item.height = 22;
            item.width = 22;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("EarthenEssence"));

            recipe.AddIngredient(mod.ItemType("MagmaticEssence"));

            recipe.AddIngredient(mod.ItemType("GlacialEssence"));

            recipe.AddIngredient(mod.ItemType("LunarEssence"));

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }



    }
}
