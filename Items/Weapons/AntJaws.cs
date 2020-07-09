using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class AntJaws : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("nom nom");
        }

        public override void SetDefaults()
        {
            item.damage = 16;
            item.ranged = true;

            item.useTime = 14;
            item.useAnimation = 14;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 30000;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true; ;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AntBiterJaws");
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 14;
        }



        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

           
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY + Main.rand.Next(-4, 4), mod.ProjectileType("AntBiterJaws"), damage, knockBack, player.whoAmI);
            
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntJaw"), 20);
            recipe.AddIngredient(mod.ItemType("AntEssence"), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}