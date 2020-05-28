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
    public class GunBarrel6 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zapper");
            Tooltip.SetDefault("Shoots balls of electricity that explode into mini sparks");
            

        }
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 26;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunBarrel = true;
            AttachmentTier = 6;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LunarScrew"), 15);
            recipe.AddIngredient(mod.ItemType("LunarPlate"), 12);
            recipe.AddIngredient(mod.ItemType("LunarCog"), 8);
            recipe.AddIngredient(mod.ItemType("LunarPipe"), 16);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
