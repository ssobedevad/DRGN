using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class VoidBlade : ModItem
    {
       
        public override void SetDefaults()
        {
            item.damage = 70;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = 950000;
            item.rare = ItemRarityID.Yellow;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("VoidBladeProj");
            item.channel = true;
           
            item.noUseGraphic = true;
            item.noMelee = true;
            item.useTurn = true;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidKey"));

            recipe.AddTile(ModContent.TileType<Tiles.VoidChest>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }




    }
}
        
    
