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
    public class GunGrip7 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flarium Grip");
            Tooltip.SetDefault("decreases spread slightly and increases attack speed and damage greatly");
            

        }
        public override void SetDefaults()
        {
            item.width = 19;
            item.height = 11;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunGrip = true;
            AttachmentTier = 7;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("FlariumScrew"), 12);
            recipe.AddIngredient(mod.ItemType("FlariumPlate"), 15);
            recipe.AddIngredient(mod.ItemType("FlariumCog"), 10);
            recipe.AddIngredient(mod.ItemType("FlariumPipe"), 14);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
