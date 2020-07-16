using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DRGN.Items.Weapons
{
    public class GalactiteRifle : ModItem
    {

        private int fireSpeed = 18;
        public override void SetDefaults()
        {
            item.damage = 145;
            item.ranged = true;
            item.channel = true;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 508000;
            item.rare = ItemRarityID.Purple;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true; ;
            item.noMelee = true;
            item.shoot = ProjectileID.MoonlordBullet;
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 16;
        }



        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (fireSpeed > 4) //change the firespeed
            {
                fireSpeed -= 1;
            }

            Vector2 norm = Vector2.Normalize(new Vector2(speedX, speedY));
            position += norm * 35;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.NextFloat(-0.5f, 0.5f), ProjectileID.VortexBeaterRocket, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.NextFloat(-0.5f, 0.5f), ProjectileID.BlackBolt, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.NextFloat(-0.5f, 0.5f), mod.ProjectileType("GalactiteStarLaser"), damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.NextFloat(-0.5f, 0.5f), mod.ProjectileType("GalactiteStarLaser"), damage, knockBack, player.whoAmI);
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
                fireSpeed = 18;
                item.useTime = fireSpeed;
                item.useAnimation = fireSpeed;

            }

            base.HoldItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidRifle"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 15);
            recipe.AddIngredient(mod.ItemType("GalacticScale"),10);
            
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}