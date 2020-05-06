using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class AimOfGods : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("literraly impossible to miss");
        }

        public override void SetDefaults()
        {
            item.damage = 100;
            item.ranged = true;

            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 100000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AutoAim");
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 20;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ShroomiteBar,15);
            recipe.AddIngredient(mod.ItemType("Godslayer"));
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        
    }
}