using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class MagmaticHuntingRifle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Click click pow");
        }

        public override void SetDefaults()
        {
            item.damage = 500;
            item.ranged = true;

            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.height = 16;
            item.width = 64;

            item.noMelee = true;
            item.shoot = mod.ProjectileType("MagmaticBullet");
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 180;
        }



        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 20);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}