﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Items.EngineerClass.Attachments
{
    public class GunBarrel9 : EngineerAttachments
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void phantom kinves");
            Tooltip.SetDefault("Shoots void phantom knives that create void shadows on enemy hit");
            

        }
        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 26;
            item.maxStack = 1;
            item.rare = 4;
            item.value = 100;
            isGunBarrel = true;
            AttachmentTier = 9;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidScrew"), 15);
            recipe.AddIngredient(mod.ItemType("VoidPlate"), 12);
            recipe.AddIngredient(mod.ItemType("VoidCog"), 8);
            recipe.AddIngredient(mod.ItemType("VoidPipe"), 16);
            recipe.AddTile(TileID.WorkBenches);


            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}