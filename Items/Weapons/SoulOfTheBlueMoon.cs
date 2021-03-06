﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using DRGN.Rarities;

namespace DRGN.Items.Weapons
{
    public class SoulOfTheBlueMoon : ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Zapp");
        }

        public override void SetDefaults()
        {
            item.damage = 195;
            item.magic = true;
            
            item.useTime = 15;
            item.useAnimation = 15;
            item.autoReuse = true;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 128000;
            item.rare = ItemRarities.DarkBlue;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("BlueStarProj");
            item.mana = 16;
            item.crit = 2;
            item.shootSpeed = 12;

        }
        
       
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(mod.ItemType("ElementalControl"));
            recipe.AddIngredient(ItemID.LastPrism);
            recipe.AddIngredient(ItemID.NebulaArcanum);
             recipe.AddIngredient(ItemID.NebulaBlaze);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

}

    
