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
    public class GunScope5 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultra Scope");
            Tooltip.SetDefault("Increases crit chance by 35% reduces spread considerably and increases base damage and attack speed");
            

        }
        public override void SetDefaults()
        {
            item.width = 27;
            item.height = 17;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunScope = true;
            AttachmentTier = 5;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LunarScrew"), 12);
            recipe.AddIngredient(mod.ItemType("LunarPlate"), 15);
            recipe.AddIngredient(mod.ItemType("LunarCog"), 10);
            recipe.AddIngredient(mod.ItemType("LunarPipe"), 14);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
