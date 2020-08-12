

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
namespace DRGN.Items.Weapons.ReaperWeapons.Scythes
{
    public class GlacialReaver : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Right Click to throw a returning scythe towards the mouse and jump backwards");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 36;

            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5.5f;
            item.value = 110000;
            item.rare = ItemRarityID.LightRed;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            type = Scythe;
            item.useTurn = true;
            DashSpeed = 6f;
            item.useTurn = true;
            scytheThrowStyle = 0;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-15, 15), Main.rand.Next(-15, 15), mod.ProjectileType("IceShatter"), damage, knockBack, player.whoAmI);
            target.AddBuff(BuffID.Frostburn, 60);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlacialBar"), 16);
            recipe.AddIngredient(mod.ItemType("GlacialShard"), 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}