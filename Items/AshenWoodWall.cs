﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Items
{
    public class AshenWoodWall : ModItem
    {

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = ItemRarityID.Orange;
            item.value = 1000;
            item.consumable = true;
            item.createWall = mod.WallType("AshenWoodWall");
            item.autoReuse = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AshenWood"));
            recipe.SetResult(this,4);
            recipe.AddRecipe();          
        }
    }
}
