

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
namespace DRGN.Items.Weapons
{
    public class DragonFlySlicer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("snapped_handle"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Releases dragonflies on hit");
        }

        public override void SetDefaults()
        {
            item.damage = 240;
            item.melee = true;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 7f;
            item.value = 180000;
            item.rare = ItemRarities.DarkBlue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 4;
            item.useTurn = true;

        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            for (int i = 0; i < 3; i++)
            { Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-7, 7), Main.rand.Next(-7, 7), mod.ProjectileType("DragonFlyJaws"), item.damage, item.knockBack, player.whoAmI); }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<ElementalJaw>(), 12);
            recipe.AddIngredient(ModContent.ItemType<DragonFlyDust>(), 12);
            recipe.AddIngredient(ModContent.ItemType<DragonFlyWing>(), 12);


            recipe.AddTile(ModContent.TileType<InterGalacticAnvilTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}