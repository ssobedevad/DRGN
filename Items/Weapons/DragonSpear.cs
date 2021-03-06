﻿
using DRGN.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class DragonSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Ignites enemies on hit");
        }

        public override void SetDefaults()
        {
            item.damage = 330;
            item.useStyle = 1;
            item.useAnimation = 16;
            item.useTime = 22;
            item.shootSpeed = 15f;
            item.knockBack = 6.5f;
            item.width = 32;
            item.height = 32;
            item.scale = 1f;
            item.value = 240000;
            item.rare = ItemRarities.FieryOrange;
            item.thrown = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("DragonSpearProj");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 12);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 12);
           
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}