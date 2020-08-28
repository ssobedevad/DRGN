
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class ShadeThrowingKnife : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 12;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 30;
            item.useTime = 30;
            item.shootSpeed = 14f;
            item.knockBack = 3.5f;
            item.value = 5000;
            item.rare = ItemRarityID.Green;
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("ShadeThrowingKnife");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShadowThrowingKnife"));
            recipe.AddIngredient(mod.ItemType("ShadeCrystal"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}