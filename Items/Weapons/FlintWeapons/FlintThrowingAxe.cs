
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons.FlintWeapons
{
    public class FlintThrowingAxe : ModItem
    {
       

        public override void SetDefaults()
        {
            item.damage = 26;
            item.useStyle = 1;
            item.useAnimation = 40;
            item.useTime = 40;
            item.shootSpeed = 10f;
            item.knockBack = 10f;
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
            item.autoReuse = false; 

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("FlintThrowingAxe");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Flint"),10);
            recipe.AddIngredient(ItemID.Wood,5);
            

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this,20);
            recipe.AddRecipe();
        }

    }
}