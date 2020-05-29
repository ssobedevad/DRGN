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
    public class GunBody6 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Insectium Body");
            Tooltip.SetDefault("increases fire rate by A LOT but increases spread slightly and increases damage very considerably greatly");
            

        }
        public override void SetDefaults()
        {
            item.width = 41;
            item.height = 16;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunBody = true;
            AttachmentTier = 6;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("InsectiumScrew"), 12);
            recipe.AddIngredient(mod.ItemType("InsectiumPlate"), 15);
            recipe.AddIngredient(mod.ItemType("InsectiumCog"), 10);
            recipe.AddIngredient(mod.ItemType("InsectiumPipe"), 14);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
