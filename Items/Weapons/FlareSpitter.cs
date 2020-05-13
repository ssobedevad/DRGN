using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class FlareSpitter : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Boom");
        }

        public override void SetDefaults()
        {
            item.damage = 115;
            item.ranged = true;

            item.useTime = 13;
            item.useAnimation = 13;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 1000000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("FlareArrow");
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 25;
            item.crit = 16;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 35);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 35);
            recipe.AddIngredient(mod.ItemType("AntiDraconianBeast"));
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            for (int i = 0; i <3; i++)
            {

                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-1, 1), mod.ProjectileType("FlareArrow"), damage, knockBack, player.whoAmI);
                }
                else { Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-1, 1), type, damage, knockBack, player.whoAmI); }
            

        }


            return false;
        }
    }
}