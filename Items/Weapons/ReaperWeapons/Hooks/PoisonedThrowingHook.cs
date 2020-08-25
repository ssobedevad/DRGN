

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
    public class PoisonedThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 13;
            item.useTime = 28;
            item.knockBack = 2.5f;
            item.value = 25000;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("PoisonHook");
            item.shootSpeed = 9.95f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("EarthenBar"), 16);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"), 16);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}