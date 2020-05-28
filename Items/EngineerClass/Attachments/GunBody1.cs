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
    public class GunBody1 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metal Body");
            Tooltip.SetDefault("Reduces spread considerably");
            

        }
        public override void SetDefaults()
        {
            item.width = 41;
            item.height = 16;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunBody = true;
            AttachmentTier = 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Screw"), 15);
            recipe.AddIngredient(mod.ItemType("MetalPlate"), 12);
            recipe.AddIngredient(mod.ItemType("Cog"), 8);
            recipe.AddIngredient(mod.ItemType("MetalPipe"), 16);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
