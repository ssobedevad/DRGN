﻿
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Weapons
{
    public class MechaShuriken : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Throws shurikens that pierce enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 46;
            item.useStyle = 1;
            item.useAnimation = 10;
            item.useTime = 10;
            item.shootSpeed = 16f;
            item.knockBack = 6.5f;
            item.width = 22;
            item.height = 22;
            item.scale = 1f;
            item.value = 42000;
            item.rare = ItemRarityID.LightRed;

            item.thrown = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("MechaShuriken");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlacialShuriken"));
            recipe.AddIngredient(ItemID.HallowedBar, 25);

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}