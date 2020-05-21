using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{
    
    public class BootsofJumping : ModItem
    {
        
        private static float jumpCD = 0 ;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boots of Jumping");
            Tooltip.SetDefault(" while in the air you can jump again"
                                  + "\n gives the ability to dash"
                                    + "\nYou are immune to fall damage");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value =10000;
           
            item.accessory = true;
            item.rare = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            
            player.dash = 1;
            player.noFallDmg = true;
            if(!player.releaseJump && jumpCD == 0 && player.velocity.Y >= 0 ) { player.velocity.Y = -12; jumpCD += 20f; for (int i = 0; i < 25; i++) { int DustID = Dust.NewDust(player.Center, 0, 0, 182, 0.0f, 0.0f, 10, default(Color), 1f); } }
            else if (jumpCD > 0 && player.releaseJump ) { jumpCD -= 1f; }
            
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HermesBoots);
            recipe.AddIngredient(ItemID.PinkGel,20);
            recipe.AddIngredient(ItemID.Feather,20);
            recipe.AddIngredient(ItemID.LuckyHorseshoe);


            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }








    }
}