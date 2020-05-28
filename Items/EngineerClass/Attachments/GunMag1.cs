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
    public class GunMag1 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Extended Mag");
            Tooltip.SetDefault("Increases clip size by 10");
            

        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 11;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunMag = true;
            AttachmentTier = 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Screw"), 8);
            recipe.AddIngredient(mod.ItemType("MetalPlate"), 10);
            recipe.AddIngredient(mod.ItemType("Cog"), 8);
            recipe.AddIngredient(mod.ItemType("MetalPipe"), 10);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
