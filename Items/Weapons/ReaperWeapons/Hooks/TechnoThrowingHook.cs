

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
    public class TechnoThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 44;
            item.useTime = 34;
            item.knockBack = 3.75f;
            item.value = 120000;
            item.rare = ItemRarityID.LightPurple;
            item.shoot = mod.ProjectileType("TechnoHook");
            item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TechnoBar"), 14);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}