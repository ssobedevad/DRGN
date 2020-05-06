using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class PatchworkBook : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("sort of works");
        }

        public override void SetDefaults()
        {
            item.damage = 12;
            item.magic = true;
            
            item.useTime = 20;
            item.useAnimation = 20;
            item.reuseDelay = 20;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 100;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = ProjectileID.BallofFire;
            item.mana = 2;
            item.crit = 7;
            item.shootSpeed = 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk,5);
            
            recipe.AddIngredient(mod.ItemType("TornBook"));
           
            
            
          
          
        recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}