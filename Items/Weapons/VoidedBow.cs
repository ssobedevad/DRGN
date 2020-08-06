using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using DRGN.Projectiles;

namespace DRGN.Items.Weapons
{
    public class VoidedBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Turns Wooden arrows into voided arrows");
        }

        public override void SetDefaults()
        {
            item.damage = 36;
            item.ranged = true;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 100000;
            item.rare = ItemRarityID.LightPurple;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("VoidedArrow");
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 10;
        }
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), item.shoot, damage, knockBack, player.whoAmI, -2);
            }
            else { return true; }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidKey"));

            recipe.AddTile(ModContent.TileType<Tiles.VoidChest>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}