﻿
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class LunarShuriken : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Throws shurikens that create lunar flares on enemy hit");
        }

        public override void SetDefaults()
        {
            item.damage = 90;
            item.useStyle = 1;
            item.useAnimation = 9;
            item.useTime = 9;
            item.shootSpeed = 16f;
            item.knockBack = 6.5f;
            item.width = 22;
            item.height = 22;
            item.scale = 1f;
            item.value = 250000;
            item.rare = ItemRarityID.Red;

            item.thrown = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("LunarShuriken");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShroomiteShuriken"));
            recipe.AddIngredient(mod.ItemType("LunarFragment"), 10);
            recipe.AddIngredient(mod.ItemType("LunarEssence"), 10);

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}