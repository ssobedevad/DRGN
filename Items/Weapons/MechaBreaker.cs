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
            item.damage = 45;
            item.ranged = true;

            item.useTime = 32;
            item.useAnimation = 32;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("GlassArrow");
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 14;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar, 12);
            recipe.AddIngredient(mod.ItemType("HellHornBow"));
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.TitaniumBar, 12);
            recipe2.AddIngredient(mod.ItemType("HellHornBow"));
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
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