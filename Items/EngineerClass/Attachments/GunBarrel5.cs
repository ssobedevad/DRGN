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
    public class GunBarrel5 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MiniGun");
            Tooltip.SetDefault("Shoots shoots very quickly with large spread and a 75% chance not to consume ammo");
            

        }
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 26;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunBarrel = true;
            AttachmentTier = 5;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("PlantenScrew"), 15);
            recipe.AddIngredient(mod.ItemType("PlantenPlate"), 12);
            recipe.AddIngredient(mod.ItemType("PlantenCog"), 8);
            recipe.AddIngredient(mod.ItemType("PlantenPipe"), 16);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
