using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Buffs;
using DRGN.Tiles;

namespace DRGN.Items.Weapons
{
    public class CosmoBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Releases lunar fragments on hit");
        }

        public override void SetDefaults()
        {
            item.damage = 82;
            item.melee = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 7f;
            item.value = 18000;
            item.rare = ItemRarityID.Yellow;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 4;
            item.useTurn = true;

        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(target.position.X, target.position.Y, Main.rand.Next(-5,5), Main.rand.Next(-5, 5), mod.ProjectileType("CelestialSwarm"), item.damage, item.knockBack, Main.myPlayer);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<CosmoBar>(), 12);
            recipe.AddIngredient(ItemID.SpectreBar, 12);


            recipe.AddTile(ModContent.TileType<InterGalacticAnvilTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}