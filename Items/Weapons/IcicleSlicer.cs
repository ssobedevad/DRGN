using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Buffs;

namespace DRGN.Items.Weapons
{
    public class IcicleSlicer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Releases icicles on hit");
        }

        public override void SetDefaults()
        {
            item.damage = 48;
            item.melee = true;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5f;
            item.value = 18000;
            item.rare = ItemRarityID.Pink;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 2;
            item.useTurn = true;

        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
             Projectile.NewProjectile(target.Center.X, target.Center.Y - 500, (float)(Main.rand.Next(-100, 100)) / 100f, 5, mod.ProjectileType("Icicle"), 50, 1f, player.whoAmI); 
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<GlacialShard>(), 12);
            recipe.AddIngredient(ModContent.ItemType<GlacialBar>(), 12);


            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}