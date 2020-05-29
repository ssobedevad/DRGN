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
    public class GunGrip8 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Grip");
            Tooltip.SetDefault("Decreases spread greatly and increases attack speed and damage very greatly");
            

        }
        public override void SetDefaults()
        {
            item.width = 19;
            item.height = 11;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunGrip = true;
            AttachmentTier = 8;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidScrew"), 12);
            recipe.AddIngredient(mod.ItemType("VoidPlate"), 15);
            recipe.AddIngredient(mod.ItemType("VoidCog"), 10);
            recipe.AddIngredient(mod.ItemType("VoidPipe"), 14);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
