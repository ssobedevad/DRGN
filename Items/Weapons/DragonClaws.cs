
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class DragonClaws : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 14;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 8;
            item.useTime = 8;
            item.knockBack = 3.5f;
            item.value = 5000;
            item.rare = ItemRarityID.Orange;
            item.melee = true;
            item.autoReuse = true;
            item.useTurn = true;
            item.UseSound = SoundID.Item1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 16);
            recipe.AddIngredient(mod.ItemType("FlareCrystal"), 18);
            recipe.AddIngredient(mod.ItemType("AshenWood"), 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}