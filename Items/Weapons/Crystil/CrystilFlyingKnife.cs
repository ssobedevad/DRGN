
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons.Crystil
{
    public class CrystilFlyingKnife : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 38;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 12;
            item.useTime = 12;
            item.shootSpeed = 16f;
            item.knockBack = 6.5f;
            item.value = 120000;
            item.rare = ItemRarityID.Purple;
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("CrystilFlyingKnife");
        }      
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CrystilBar"), 8);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}