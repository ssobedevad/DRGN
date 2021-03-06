﻿using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class ArcticHuntingRifle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Click click pow");
        }

        public override void SetDefaults()
        {
            item.damage = 155;
            item.ranged = true;

            item.useTime = 55;
            item.useAnimation = 55;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 60000;
            item.rare = ItemRarityID.Pink;
            item.UseSound = SoundID.Item1;
            item.height = 16;
            item.width = 64;
           
            item.noMelee = true;
            item.shoot = mod.ProjectileType("IceBullet");
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 18f;
        }



       
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlacialShard"), 20);
            recipe.AddIngredient(mod.ItemType("GlacialBar"), 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}