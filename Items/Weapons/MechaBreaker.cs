using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class MechaBreaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires two strong arrows that become glass arrows if wooden arrows are used");
        }

        public override void SetDefaults()
        {
            item.damage = 26;
            item.ranged = true;

            item.useTime = 55;
            item.useAnimation = 55;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("GlassArrow");
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 15;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("DRGN:T3HmB", 6);
            recipe.AddRecipeGroup("DRGN:T1Rep");
            recipe.AddRecipeGroup("DRGN:T2Rep");
            recipe.AddRecipeGroup("DRGN:T3Rep");
            recipe.AddIngredient(mod.ItemType("HellHornBow"));
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
            
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            
            for (int i = 0; i < 2; ++i)
            {
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-1,1), mod.ProjectileType("GlassArrow"), damage, knockBack, player.whoAmI);
                }
                else { Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-1, 1), type, damage, knockBack, player.whoAmI); }
            
            
            }
            return false;
        }
    }
}