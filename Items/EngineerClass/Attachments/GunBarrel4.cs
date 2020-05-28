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
    public class GunBarrel4 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Long Barrel");
            Tooltip.SetDefault("Shoots a powerful bullet dealing 200% damage but at 75% attack speed and with reduced spread");
            

        }
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 26;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunBarrel = true;
            AttachmentTier = 4;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("IcyScrew"), 15);
            recipe.AddIngredient(mod.ItemType("IcyPlate"), 12);
            recipe.AddIngredient(mod.ItemType("IcyCog"), 8);
            recipe.AddIngredient(mod.ItemType("IcyPipe"), 16);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
