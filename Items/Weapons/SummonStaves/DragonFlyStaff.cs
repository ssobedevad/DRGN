﻿using DRGN.Rarities;
using DRGN.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DRGN.Items.Weapons.SummonStaves
{
    public class DragonFlyStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Fly Staff");
            Tooltip.SetDefault("The dragonflies are your army");
        }
        public override void SetDefaults()
        {
            item.damage = 155;
            item.summon = true;
            item.rare = ItemRarities.DarkBlue;
            item.value = 200000;
            item.useTime = 25;
            item.useAnimation = 25;
            item.buffType = mod.BuffType("DragonFlyMinion");
            item.shoot = mod.ProjectileType("DragonFlyMinion");
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;

        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(item.buffType, 2, true);
            position = Main.MouseWorld;

            return true;

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
