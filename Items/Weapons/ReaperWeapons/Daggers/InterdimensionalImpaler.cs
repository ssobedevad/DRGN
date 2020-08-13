

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
    public class InterdimensionalImpaler : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Allows you to right click hunted enemies throw a riocheting dagger at them");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 255;
            BloodHuntRange = 250;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 12f;
            item.value = 2000000;
            item.rare = ItemRarities.VoidPurple;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            type = Dagger;
            item.useTurn = true;
            DashSpeed = 9.5f;
            item.shootSpeed = 12f;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(target.Center, Vector2.Zero, mod.ProjectileType("VoidExplosion"), damage, 0f, player.whoAmI);
            Projectile.NewProjectile(target.Center, new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), mod.ProjectileType("VoidedArrow"),damage, 0f, player.whoAmI, 1);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidBar"), 16);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 16);

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }




    }
}