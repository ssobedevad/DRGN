

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
    public class RockThrowingHook : ReaperWeapon
    {
        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Hitting enemies leaves a hook that can be retracted with right click");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 65;

            item.useTime = 38;
            item.useAnimation = 38;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 9f;
            item.value = 180000;
            item.rare = ItemRarityID.Lime;
            item.UseSound = SoundID.Item1;
            item.shootSpeed = 10f;
            item.autoReuse = false;
            item.noUseGraphic = true;
            item.noMelee = true;
            type = Hook;
            projectileText = ModContent.GetTexture("DRGN/Projectiles/Reaper/Hooks/RockHook");
            chaintext = ModContent.GetTexture("DRGN/Projectiles/Reaper/Chains/ReaperChainRock");
            item.useTurn = true;
            DashSpeed = 4.5f;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(target.Center, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), ModContent.ProjectileType<RockShot>(), damage, knockBack, player.whoAmI);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LihzahrdBar"), 16);

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}