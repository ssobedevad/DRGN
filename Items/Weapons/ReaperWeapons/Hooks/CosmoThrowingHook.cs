

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
    public class CosmoThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 68;
            item.useTime = 28;
            item.knockBack = 6f;
            item.value = 350000;
            item.rare = ItemRarityID.Cyan;
            item.shoot = mod.ProjectileType("CosmoHook");
            item.shootSpeed = 11.5f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CosmoBar"), 18);
            recipe.AddIngredient(ItemID.SpectreBar, 16);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}