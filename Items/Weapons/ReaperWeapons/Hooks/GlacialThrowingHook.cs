

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
    public class GlacialThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 32;
            item.useTime = 32;
            item.knockBack = 3.75f;
            item.value = 110000;
            item.rare = ItemRarityID.LightRed;
            item.shoot = mod.ProjectileType("GlacialHook");
            item.shootSpeed = 10f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlacialBar"), 15);
            recipe.AddIngredient(mod.ItemType("GlacialShard"), 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}