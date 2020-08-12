

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
using DRGN.Projectiles.Reaper;

namespace DRGN.Items.Weapons.ReaperWeapons.Hooks
{
    public class CosmoThrowingHook : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Hitting enemies leaves a hook that can be retracted with right click");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 70;

            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6f;
            item.value = 650000;
            item.rare = ItemRarityID.Cyan;
            item.UseSound = SoundID.Item1;
            item.shootSpeed = 12.5f;
            item.autoReuse = false;
            item.noUseGraphic = true;
            item.noMelee = true;
            type = Hook;
            projectileText = ModContent.GetTexture("DRGN/Projectiles/Reaper/Hooks/CosmoHook");
            chaintext = ModContent.GetTexture("DRGN/Projectiles/Reaper/Chains/ReaperChainCosmo");
            item.useTurn = true;
            DashSpeed = 4f;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(target.position.X, target.position.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), mod.ProjectileType("CelestialSwarm"), item.damage, item.knockBack, player.whoAmI);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CosmoBar"), 18);
            recipe.AddIngredient(ItemID.SpectreBar, 16);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}