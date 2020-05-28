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
    public class GunChamber2 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Golden Mechanism");
            Tooltip.SetDefault("Increases damage and fire rate considerably");
            

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 11;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunChamber = true;
            AttachmentTier = 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GoldenScrew"), 12);
            recipe.AddIngredient(mod.ItemType("GoldenPlate"), 15);
            recipe.AddIngredient(mod.ItemType("GoldenCog"), 10);
            recipe.AddIngredient(mod.ItemType("GoldenPipe"), 14);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
