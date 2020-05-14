﻿using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class OmniBow : ModItem
    {
      
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("pretty cool");
        }

        public override void SetDefaults()
        {
            item.damage = 120;
            item.ranged = true;
            
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 1;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AutoAim");
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 22;
            item.crit = 20;
        }
        public override bool AltFunctionUse(Player player)//You use this to allow the item to be right clicked
        {
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidBar"),35);
            recipe.AddIngredient(mod.ItemType("VoidSilk"), 35);
            recipe.AddIngredient(mod.ItemType("FlareSpitter"));
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-1, 1), mod.ProjectileType("FlareArrow"), damage, knockBack, player.whoAmI);
            }
            else { Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-1, 1), type, damage, knockBack, player.whoAmI); }



            return false;
        }
    }
}