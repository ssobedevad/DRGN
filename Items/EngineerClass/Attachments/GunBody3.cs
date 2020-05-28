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
    public class GunBody3 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icy Body");
            Tooltip.SetDefault("decreases fire rate but decreases spread and increases damage");
            

        }
        public override void SetDefaults()
        {
            item.width = 41;
            item.height = 16;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunBody = true;
            AttachmentTier = 3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("IcyScrew"), 12);
            recipe.AddIngredient(mod.ItemType("IcyPlate"), 15);
            recipe.AddIngredient(mod.ItemType("IcyCog"), 10);
            recipe.AddIngredient(mod.ItemType("IcyPipe"), 14);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
