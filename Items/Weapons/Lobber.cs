﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class Lobber : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Releases bees on hit");
        }

        public override void SetDefaults()
        {
            item.damage = 12;
            item.useStyle = 5;
            item.useAnimation = 22;
            item.useTime = 22;
            item.shootSpeed = 12f;
            item.knockBack = 6.5f;
            item.width = 37;
            item.height = 16;
            item.scale = 1f;
            item.value = 5000;
            item.rare = ItemRarityID.Green;
            item.noMelee = true;
            item.ranged = true;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("FlySpit");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"), 16);
            recipe.AddIngredient(mod.ItemType("EarthenBar"), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}