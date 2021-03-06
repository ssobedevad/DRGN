﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class FireVolley : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots a volley of bouncing fire balls");
        }
        
        public override void SetDefaults()
        {
            item.damage = 25;
            item.magic = true;
            item.mana = 20;
            item.useTime = 64;
            item.useAnimation = 64;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 20000;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true; ;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AntBiterJaws");
            
            item.shootSpeed = 15;
            
        }



        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            
                Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.NextFloat(-1.5f, 1.5f), speedY + Main.rand.NextFloat(-1.5f,1.5f),mod.ProjectileType("FireVolley"), damage, knockBack, player.whoAmI,7);
                 
            
           
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar,10);
            recipe.AddIngredient(ItemID.Book,5);
            recipe.AddIngredient(ItemID.LavaBucket,5);
            recipe.AddIngredient(ItemID.WaterBolt);
            recipe.AddIngredient(mod.ItemType("FlareCrystal"), 12);
            recipe.AddTile(TileID.Bookcases);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}