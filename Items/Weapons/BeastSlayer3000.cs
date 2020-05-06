using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class BeastSlayer3000 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("three times the pain but more than 3 times the gain");
        }

        public override void SetDefaults()
        {
            item.damage = 115;
            item.ranged = true;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 150000;
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AutoAim");
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 20;
            item.crit = 10;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentVortex, 25);
            recipe.AddIngredient(mod.ItemType("AimOfGods"));
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {


            
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY+2, type, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY-2, type, damage, knockBack, player.whoAmI);


            return false;
        }
    }
}