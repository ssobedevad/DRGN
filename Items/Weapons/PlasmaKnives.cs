﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
namespace DRGN.Items.Weapons
{
    public class PlasmaKnives : ModItem
    { private Vector2 projVel;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Rains plasma knives");
        }

        public override void SetDefaults()
        {
            item.damage = 125;
            item.magic = true;
            item.autoReuse = true;
            item.useTime = 8;
            item.useAnimation = 8;
            
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 450000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("PlasmaKnife");
            item.mana = 4;
            item.crit = 20;
            item.shootSpeed = 16;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 15);
            recipe.AddIngredient(mod.ItemType("CosmoBar"),15);
            recipe.AddIngredient(mod.ItemType("DragonFlyDust"), 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position.Y -= 600;
            ShootTo(position);
            for (int i = 0; i < 3; ++i)
            {
                
                Projectile.NewProjectile(position.X, position.Y, projVel.X + ((float)Main.rand.Next(-100,100)/100f),projVel.Y , type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        private void ShootTo(Vector2 position)
        {
            // Sets the max speed of the npc.



            projVel = Main.MouseWorld - position;
            float magnitude = Magnitude(projVel);
            if (magnitude > item.shootSpeed)
            {
                projVel *= item.shootSpeed / magnitude;
            }
            





        }
        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }



    }
}