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
    public class GunBarrel3 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blast Barrel");
            Tooltip.SetDefault("Shoots a huge spray of  bullets at once but each dealing 30% damage and reduces attack speed by 15% and with massive spread");
            

        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunBarrel = true;
            AttachmentTier = 3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GoldenScrew"), 15);
            recipe.AddIngredient(mod.ItemType("GoldenPlate"), 12);
            recipe.AddIngredient(mod.ItemType("GoldenCog"), 8);
            recipe.AddIngredient(mod.ItemType("GoldenPipe"), 16);
            recipe.AddTile(TileID.WorkBenches);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
