﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using DRGN.Rarities;

namespace DRGN.Items.Weapons
{
    public class Bomber : ModItem
    { 
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Shoots big bombs that explodes into a shrapnel of smaller ones");
        }

        public override void SetDefaults()
        {
            item.damage = 390;
            item.magic = true;
            item.autoReuse = true;
            item.useTime = 30;
            item.useAnimation = 30;
            
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 150000;
            item.rare = ItemRarities.FieryOrange;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("BiggerBomb");
            item.mana = 14;
            item.crit = 1;
            item.shootSpeed = 16;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(mod.ItemType("SolariumBar"),15);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 10);
            recipe.AddIngredient(mod.ItemType("AntCrawlerScale"), 10);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
        



    }
}