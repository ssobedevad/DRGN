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
    public class GunScope7 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scope of the Inferno");
            Tooltip.SetDefault("Increases crit chance by 50% and attack speed and damage considerably");
            

        }
        public override void SetDefaults()
        {
            item.width = 27;
            item.height = 17;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunScope = true;
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
