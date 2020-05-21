using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{

    public class CrystalofCharisma : ModItem
    {

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal of Charisma");
            Tooltip.SetDefault("Grants half a second chance when you need it");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 10000;
            item.expert = true;
            item.accessory = true;
            
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BoneGlove);
            recipe.AddIngredient(mod.ItemType("PowderofCourage")); 
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}