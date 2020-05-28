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
    public class GunChamber1 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metallic Mechanism");
            Tooltip.SetDefault("Increases damage and increases fire rate");
            

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 11;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunChamber = true;
            AttachmentTier = 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Screw"), 20);
            recipe.AddIngredient(mod.ItemType("MetalPlate"), 12);
            recipe.AddIngredient(mod.ItemType("Cog"), 15);
            recipe.AddIngredient(mod.ItemType("MetalPipe"), 12);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
