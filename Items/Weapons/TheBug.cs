using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class TheBug : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Turns musket balls into glitched bullets");
        }

        public override void SetDefaults()
        {
            item.damage = 23;
            item.ranged = true;

            item.useTime = 11;
            item.useAnimation = 11;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 73000;
            item.rare = ItemRarityID.LightPurple;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("GlitchBullet");
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 16f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 norm = Vector2.Normalize(new Vector2(speedX, speedY));
            position += norm * 30;
            if (type == ProjectileID.Bullet)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.NextFloat(-1.5f, 1.5f), item.shoot, damage, knockBack, player.whoAmI);
            }
            else
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.NextFloat(-1.5f, 1.5f), type, damage, knockBack, player.whoAmI);
            }

            return false;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("TechnoBar"), 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}