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
    public class GunBarrel2 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ShotGun Barrel");
            Tooltip.SetDefault("Shoots a volley of  bullets at once but each dealing 50% damage");
            

        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunBarrel = true;
            AttachmentTier = 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Screw"), 12);
            recipe.AddIngredient(mod.ItemType("MetalPlate"), 15);
            recipe.AddIngredient(mod.ItemType("Cog"), 10);
            recipe.AddIngredient(mod.ItemType("MetalPipe"), 14);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
