using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class LunarRifle : ModItem
    {

        private int fireSpeed = 22;
        public override void SetDefaults()
        {
            item.damage = 70;
            item.ranged = true;
            item.channel = true;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 18000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true; ;
            item.noMelee = true;
            item.shoot = ProjectileID.MoonlordBullet;
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 14;
        }



        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (fireSpeed > 12) //change the firespeed
            {
                fireSpeed -= 1;
            }

            Vector2 norm = Vector2.Normalize(new Vector2(speedX, speedY));
            position += norm * 35;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.NextFloat(-1, 1),ProjectileID.VortexBeaterRocket, damage, knockBack, player.whoAmI);
            return true;
        }
        public override void HoldItem(Player player)
        {

            if (player.channel == true)
            {

                item.useTime = fireSpeed;
                item.useAnimation = fireSpeed;

            }
            else if (player.channel == false)
            {
                fireSpeed = 22;
                item.useTime = fireSpeed;
                item.useAnimation = fireSpeed;

            }

            base.HoldItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LunarFragment"), 20);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"), 5);
            recipe.AddIngredient(mod.ItemType("LunarStar"), 5);
            recipe.AddIngredient(ItemID.VortexBeater);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}