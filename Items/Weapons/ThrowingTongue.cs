﻿using DRGN.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class ThrowingTongue : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Rapidly flails tongues at enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 22;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.thrown = true;
            item.channel = true; //Channel so that you can held the weapon [Important]
            item.shoot = mod.ProjectileType("FrogTongue");
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.UseSound = SoundID.Item13;
            item.useStyle = 5;
            item.shootSpeed = 14f;
            item.useAnimation = 20;
            item.value = 18000;
            item.rare = ItemRarityID.Green;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"), 14);
            recipe.AddIngredient(mod.ItemType("EarthenBar"), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
