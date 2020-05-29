using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Items.EngineerClass.Attachments
{
    public class GunBarrel7 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phantom kinves");
            Tooltip.SetDefault("Shoots phantom knives that create shadows on enemy hit");
            

        }
        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 26;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunBarrel = true;
            AttachmentTier = 7;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("InsectiumScrew"), 15);
            recipe.AddIngredient(mod.ItemType("InsectiumPlate"), 12);
            recipe.AddIngredient(mod.ItemType("InsectiumCog"), 8);
            recipe.AddIngredient(mod.ItemType("InsectiumPipe"), 16);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
