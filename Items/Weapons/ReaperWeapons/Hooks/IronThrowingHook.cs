

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
    public class IronThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 6;
            item.shootSpeed = 8.5f;
            item.knockBack = 1.5f;
            item.value = 200;
            item.rare = ItemRarityID.Blue;
            item.shoot = mod.ProjectileType("IronHook");
            item.useTime = 30;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 14);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}