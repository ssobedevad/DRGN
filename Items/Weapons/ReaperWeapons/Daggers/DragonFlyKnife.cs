

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
namespace DRGN.Items.Weapons.ReaperWeapons.Daggers
{
    public class DragonFlyKnife : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Allows you to right click hunted enemies throw a riocheting dagger at them");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 190;
            BloodHuntRange = 200;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 9.5f;
            item.value = 1200000;
            item.rare = ItemRarities.DarkBlue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            type = Dagger;
            item.useTurn = true;
            DashSpeed = 9f;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("DragonFlyJaws"),damage, knockBack, player.whoAmI);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonFlyDust"), 14);
            recipe.AddIngredient(mod.ItemType("DragonFlyWing"), 14);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"), 6);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }
}