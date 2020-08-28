
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class ShadowThrowingKnife : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 18;
            item.useTime = 18;
            item.shootSpeed = 12f;
            item.knockBack = 3.5f;
            item.value = 5000;
            item.rare = ItemRarityID.Green;
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true; 
            item.autoReuse = true; 
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("ShadowThrowingKnife");
        }
      
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<ShadowBar>(), 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}