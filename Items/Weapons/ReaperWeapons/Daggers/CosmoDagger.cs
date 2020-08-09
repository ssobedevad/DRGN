

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
    public class CosmoDagger : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Allows you to right click hunted enemies throw a riocheting dagger at them");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 98;
            BloodHuntRange = 180;
            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6.5f;
            item.value = 550000;
            item.rare = ItemRarityID.Cyan;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            type = Dagger;
            item.useTurn = true;
            DashSpeed = 8f;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(target.position.X, target.position.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("CelestialSwarm"), item.damage, item.knockBack, player.whoAmI);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LihzahrdBar"), 12);
            recipe.AddIngredient(ItemID.SpectreBar, 12);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }
}