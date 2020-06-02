using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Items.EngineerClass;

namespace DRGN.Items.Equipables
{
    
    public class BulletChain : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bullet Chain");
            Tooltip.SetDefault("Increases clip size by 15 and reduces reload time by 25%");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 100000;
            item.rare = 8;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 15;
            player.GetModPlayer<EngineerPlayer>().ReloadCounter2 = (int)(player.GetModPlayer<EngineerPlayer>().ReloadCounter2*0.75);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar,5);
            recipe.AddIngredient(mod.ItemType("MetalloidConverter"),3);
            
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}