using DRGN.Rarities;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items
{
    public class ElementalJaw : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.rare = ItemRarities.GalacticRainbow;
            item.value = 20000;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntJaw"));
            recipe.AddIngredient(mod.ItemType("ElectricAntJaw"));
            recipe.AddIngredient(mod.ItemType("FireAntJaw"));
            recipe.AddIngredient(mod.ItemType("ToxicAntJaw"));
            recipe.AddIngredient(mod.ItemType("GlacialAntJaw"));
            recipe.AddIngredient(mod.ItemType("LunarAntJaw"));
            recipe.AddIngredient(mod.ItemType("MagmaticAntJaw"));
            recipe.AddIngredient(ItemID.SoulofLight);
            recipe.AddIngredient(ItemID.SoulofNight);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 6);
            recipe.AddRecipe();
        }
    }
}
