using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class RockSprayer : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Turns regular bullets into Rockshot");
        }

        public override void SetDefaults()
        {
            item.damage = 60;
            item.ranged = true;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 6;
            item.value = 88000;
            item.rare = ItemRarityID.Lime;
            item.UseSound = SoundID.Item1;
            item.height = 16;
            item.width = 64;
            item.useTurn = false;
            item.autoReuse = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("RockShot");
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 16;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {


            if (type == ProjectileID.Bullet)
            {
                type = mod.ProjectileType("RockShot");
            }
            Vector2 initialSpeed = new Vector2(speedX-1, speedY-1);
            for (int i = 0; i < 3; i++)
            {
                

                Projectile.NewProjectile(position, initialSpeed, type, damage, knockBack, player.whoAmI);
                initialSpeed.X += 1;
                initialSpeed.Y += 1;
            }
           
            



            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("LihzahrdBar"), 12);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }





    }
}