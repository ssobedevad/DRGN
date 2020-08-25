

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
    public class DragonFlyThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 96;
            item.useTime = 28;
            item.knockBack = 12f;
            item.value = 150000;
            item.rare = ItemRarities.DarkBlue;
            item.shoot = mod.ProjectileType("DragonFlyHook");
            item.shootSpeed = 15f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonFlyDust"), 18);
            recipe.AddIngredient(mod.ItemType("DragonFlyWing"), 16);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"), 8);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}