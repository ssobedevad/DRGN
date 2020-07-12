using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class ElementalAntJaws : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots ants, electric bolts and fireballs");
        }
        private int randShot;
        public override void SetDefaults()
        {
            item.damage = 21;
            item.ranged = true;

            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 52500;
            item.rare = ItemRarityID.LightRed;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true; ;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AntBiterJaws");
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 14;
            randShot = 1;
        }



        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            if (randShot == 1)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-4, 4), mod.ProjectileType("AntBiterJaws"), damage, knockBack, player.whoAmI);
                randShot = 2;
            }
            else if (randShot == 2)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-4, 4), mod.ProjectileType("FireBallBouncyFriendly"), damage, knockBack, player.whoAmI);
                randShot = 3;
            }
            else if (randShot == 3)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-4, 4), mod.ProjectileType("MegaElectroBallFriendly"), damage, knockBack, player.whoAmI);
                randShot = 1;
            }
            if (randShot > 3 || randShot < 1) { randShot = 1; }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ElementalJaw"), 20);
            recipe.AddIngredient(mod.ItemType("AntJaws"));

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}