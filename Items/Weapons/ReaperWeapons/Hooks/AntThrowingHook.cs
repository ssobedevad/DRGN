

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
    public class AntThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 18;
            item.useTime = 34;
            item.knockBack = 4f;
            item.value = 50000;
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("AntHook");
            item.shootSpeed = 9.5f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntJaw"), 16);
            recipe.AddIngredient(mod.ItemType("AntEssence"), 16);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}