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
    public class GunScope1 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavy Scope");
            Tooltip.SetDefault("Increases accuracy and crit chance greatly but reduces fire rate by 25%");
            

        }
        public override void SetDefaults()
        {
            item.width = 49;
            item.height = 14;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunScope = true;
            AttachmentTier = 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(mod.ItemType("Screw"), 20);
            recipe.AddIngredient(mod.ItemType("MetalPlate"), 20);
            recipe.AddIngredient(mod.ItemType("Cog"), 20);
            recipe.AddIngredient(mod.ItemType("MetalPipe"), 20);
            recipe.AddTile(TileID.WorkBenches);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
