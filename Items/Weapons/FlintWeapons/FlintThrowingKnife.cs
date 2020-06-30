
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons.FlintWeapons
{
    public class FlintThrowingKnife : ModItem
    {


        public override void SetDefaults()
        {
            item.damage = 12;
            item.useStyle = 1;
            item.useAnimation = 14;
            item.useTime = 14;
            item.shootSpeed = 16f;
            item.knockBack = 3f;
            item.width = 22;
            item.height = 22;
            item.scale = 1f;
            item.rare = ItemRarityID.Blue;
            item.value = 500;
            item.maxStack = 999;
            item.consumable = true;
            item.thrown = true;
            item.noMelee = true; 
            item.noUseGraphic = true; 
            item.autoReuse = true;

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("FlintThrowingKnife");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Flint"), 6);
            recipe.AddIngredient(ItemID.Wood, 8);


            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 12);
            recipe.AddRecipe();
        }

    }
}