﻿using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class HellHornBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Turns wooden arrows into hellbats");
        }

        public override void SetDefaults()
        {
            item.damage = 21;
            item.ranged = true;

            item.useTime = 35;
            item.useAnimation = 35;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 32000;
            item.rare = ItemRarityID.LightRed;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("HellBatProj");
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 15;
        }
        public static Vector2[] randomSpread(float speedX, float speedY, int angle, int num)
        {
            var posArray = new Vector2[num];
            float spread = (float)(angle * 0.0174532925);
            float baseSpeed = (float)System.Math.Sqrt(speedX * speedX + speedY * speedY);
            double baseAngle = System.Math.Atan2(speedX, speedY);
            double randomAngle;
            for (int i = 0; i < num; ++i)
            {
                randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                posArray[i] = new Vector2(baseSpeed * (float)System.Math.Sin(randomAngle), baseSpeed * (float)System.Math.Cos(randomAngle));
            }
            return (Vector2[])posArray;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2[] speeds = randomSpread(speedX, speedY, 15, 4);
            for (int i = 0; i < 4; ++i)
            {
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    Projectile.NewProjectile(position.X, position.Y, speeds[i].X, speeds[i].Y, mod.ProjectileType("HellBatProj"), damage, knockBack, player.whoAmI);
                }
                else { Projectile.NewProjectile(position.X, position.Y, speeds[i].X, speeds[i].Y, type, damage, knockBack, player.whoAmI); }            
                }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellwingBow);
            recipe.AddIngredient(ItemID.BeesKnees);
            recipe.AddIngredient(mod.ItemType("TheNightBow"));
            recipe.AddIngredient(mod.ItemType("FlareCrystal"), 18);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
          
        }
    }
}