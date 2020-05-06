using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
namespace DRGN.Items.Weapons
{
    public class MechaSprayer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Somehow eats metal??");
        }

        public override void SetDefaults()
        {
            item.damage = 21;
            item.magic = true;
            
            item.useTime = 20;
            item.useAnimation = 20;
            item.reuseDelay = 8;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = 250000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("MechaSprayerProj");
            item.mana = 7;
            item.crit = 20;
            item.shootSpeed = 16;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MythrilBar,5);
            recipe.AddIngredient(ItemID.AdamantiteBar, 5);
            recipe.AddIngredient(mod.ItemType("DeathShower"));
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.OrichalcumBar, 5);
            recipe2.AddIngredient(ItemID.TitaniumBar, 5);
            recipe2.AddIngredient(mod.ItemType("DeathShower"));
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
            ModRecipe recipe3 = new ModRecipe(mod);
            recipe3.AddIngredient(ItemID.MythrilBar, 5);
            recipe3.AddIngredient(ItemID.TitaniumBar, 5);
            recipe3.AddIngredient(mod.ItemType("DeathShower"));
            recipe3.AddTile(TileID.WorkBenches);
            recipe3.SetResult(this);
            recipe3.AddRecipe();
            ModRecipe recipe4 = new ModRecipe(mod);
            recipe4.AddIngredient(ItemID.OrichalcumBar, 5);
            recipe4.AddIngredient(ItemID.AdamantiteBar, 5);
            recipe4.AddIngredient(mod.ItemType("DeathShower"));
            recipe4.AddTile(TileID.WorkBenches);
            recipe4.SetResult(this);
            recipe4.AddRecipe();
        }
        public static Vector2[] randomSpread(float speedX, float speedY, int angle, int num)
        {
            var posArray = new Vector2[num];
            float spread = (float)(angle * 0.0174532925);
            float baseSpeed = (float)System.Math.Sqrt(speedX * speedX + speedY * speedY);
            double baseAngle = System.Math.Atan2(speedX, speedY);
            double randomAngle;
            for (int i = 0; i < num; ++i)
            {
                randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                posArray[i] = new Vector2(baseSpeed * (float)System.Math.Sin(randomAngle), baseSpeed * (float)System.Math.Cos(randomAngle));
            }
            return (Vector2[])posArray;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2[] speeds = randomSpread(speedX, speedY, 30, 3);
            for (int i = 0; i < 3; ++i)
            {
                Projectile.NewProjectile(position.X, position.Y, speeds[i].X, speeds[i].Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}