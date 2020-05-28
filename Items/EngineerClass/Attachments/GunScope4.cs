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
    public class GunScope4 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mega Scope");
            Tooltip.SetDefault("Increases crit chance by 25% reduces spread considerably and increases base damage");
            

        }
        public override void SetDefaults()
        {
            item.width = 27;
            item.height = 17;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunScope = true;
            AttachmentTier = 4;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("PlantenScrew"), 12);
            recipe.AddIngredient(mod.ItemType("PlantenPlate"), 15);
            recipe.AddIngredient(mod.ItemType("PlantenCog"), 10);
            recipe.AddIngredient(mod.ItemType("PlantenPipe"), 14);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
