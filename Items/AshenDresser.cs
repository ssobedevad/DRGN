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
    public class AshenDresser : ModItem
    {

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.rare = ItemRarityID.Orange;
            item.value = 2500;
            item.consumable = true;
            item.createTile = mod.TileType("AshenDresser");
            item.autoReuse = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AshenWood"),8);
            recipe.AddIngredient(mod.ItemType("FlareCrystal"), 4);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
