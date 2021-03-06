﻿using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class AntiDraconianBeast : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("So ridiculous its funny");
        }

        public override void SetDefaults()
        {
            item.damage = 108;
            item.ranged = true;

            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 120000;
            item.rare = ItemRarityID.Cyan;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AutoAim");
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 22;
            item.crit = 2;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 85);
            recipe.AddIngredient(ItemID.FireworksLauncher);
            recipe.AddIngredient(ItemID.SDMG);
            recipe.AddIngredient(mod.ItemType("BeastSlayer3000"));
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {


            
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY,type, damage, knockBack, player.whoAmI);
           
            
            


            return false;
        }
    }
}