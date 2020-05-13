using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Items.Weapons
{
    public class LunarFlare : ModItem
    {
        private Vector2 Velocity;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Killin' moonlord wid style");
        }

        public override void SetDefaults()
        {
            item.damage = 185;
            item.melee = true;
            item.useTime = 14;
            item.useAnimation = 14;
            item.useStyle = 1;
            item.knockBack = 13;
            item.value = 400000;
            item.rare = 7;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 18;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("LunarFlareProj");

            item.shootSpeed = 15;
        }
        private void ShootTo(Player player)
        {
            float speed = 15f; // Sets the max speed of the npc.
            Vector2 move = Main.MouseWorld - player.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }

            Velocity = move;
        }



        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            for (int i = 0; i < Main.rand.Next(3, 5); i++)
            {
                ShootTo(player);
                Projectile.NewProjectile(position.X, position.Y, Velocity.X+Main.rand.Next(-3,3), Velocity.Y + Main.rand.Next(-3, 3), type, damage, knockBack, player.whoAmI);
            }



            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 20);
            recipe.AddIngredient(mod.ItemType("LunarFragment"),30);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}