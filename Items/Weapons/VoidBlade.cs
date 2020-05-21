using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class VoidBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Detonates enemies on hit");
        }

        public override void SetDefaults()
        {
            item.damage = 200;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = 1000000;
            item.rare = 13;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("VoidBladeProj");
            item.channel = true;
            item.scale = 2f;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.useTurn = true;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidSoul"),12);
            
            recipe.AddIngredient(mod.ItemType("VoidBar"), 15);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }
}
        
    
