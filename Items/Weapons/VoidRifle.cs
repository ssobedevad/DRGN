using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class VoidRifle : ModItem
    {

        private int fireSpeed = 22;
        public override void SetDefaults()
        {
            item.damage = 85;
            item.ranged = true;
            item.channel = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 18000;
            item.rare = ItemRarityID.Purple;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true; ;
            item.noMelee = true;
            item.shoot = ProjectileID.MoonlordBullet;
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 14;
        }



        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (fireSpeed > 6) //change the firespeed
            {
                fireSpeed -= 1;
            }

            Vector2 norm = Vector2.Normalize(new Vector2(speedX, speedY));
            position += norm * 35;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.NextFloat(-1, 1), ProjectileID.VortexBeaterRocket, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.NextFloat(-1, 1), ProjectileID.BlackBolt, damage, knockBack, player.whoAmI);
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
            recipe.AddIngredient(mod.ItemType("GalacticScale"), 5);
            recipe.AddIngredient(mod.ItemType("VoidBar"), 5);
            recipe.AddIngredient(mod.ItemType("LunarRifle"));
            recipe.AddIngredient(ItemID.OnyxBlaster);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}