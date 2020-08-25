

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
    public class LunarThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 82;
            item.useTime = 27;
            item.knockBack = 10f;
            item.value = 100000;
            item.rare = ItemRarityID.Red;
            item.shoot = mod.ProjectileType("LunarHook");
            item.shootSpeed = 15f;
        }  
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LunarFragment"), 25);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}