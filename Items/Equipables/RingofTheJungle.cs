using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{
    
    public class RingofTheJungle : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ring of the Jungle");
            Tooltip.SetDefault("Reduced potion cooldown"+"\nGrants the bonuses of the spore sac and shiny stone");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;
            item.value = 80000;
            
            item.accessory = true;
            item.expert = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.shinyStone = true;
            
            player.sporeSac = true;
            player.SporeSac();
            player.pStone = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(ItemID.ShinyStone);
            recipe.AddIngredient(ItemID.SporeSac);
            recipe.AddIngredient(ItemID.PhilosophersStone);

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}