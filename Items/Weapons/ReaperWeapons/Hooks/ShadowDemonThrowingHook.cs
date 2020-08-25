

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
    public class ShadowDemonThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 10;
            item.useTime = 24;
            item.knockBack = 2.25f;
            item.value = 8000;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("ShadowDemonHook");
            item.shootSpeed = 9.25f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShadowBar"), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}