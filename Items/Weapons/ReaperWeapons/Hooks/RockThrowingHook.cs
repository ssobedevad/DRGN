

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
    public class RockThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 63;
            item.useTime = 38;
            item.knockBack = 9f;
            item.value = 180000;
            item.rare = ItemRarityID.Lime;
            item.shoot = mod.ProjectileType("RockHook");
            item.shootSpeed = 10.5f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LihzahrdBar"), 16);

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}