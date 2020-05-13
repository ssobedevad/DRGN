using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class Godslayer : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("3.. 2.. 1.. tis gone");
        }

        public override void SetDefaults()
        {
            item.damage = 84;
            item.ranged = true;

            item.useTime = 40;
            item.useAnimation =40;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 25000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AcceleratingArrow");
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 35;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 25);
            recipe.AddIngredient(ItemID.ChlorophyteShotbow);
            recipe.AddIngredient(mod.ItemType("MechaBreaker"));
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
    }
}