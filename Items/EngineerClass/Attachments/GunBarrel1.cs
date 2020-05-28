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
    public class GunBarrel1 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Double Barrel");
            Tooltip.SetDefault("Shoots Two bullets at once but each dealing 80% damage");
            

        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunBarrel = true;
            AttachmentTier = 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("WoodenDowel"), 12);
            recipe.AddIngredient(mod.ItemType("WoodenPlate"), 15);
            recipe.AddIngredient(mod.ItemType("WoodenCog"), 10);
            recipe.AddIngredient(mod.ItemType("WoodenFixing"), 14);
            recipe.AddTile(TileID.WorkBenches);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
