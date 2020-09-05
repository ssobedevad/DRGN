

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
using DRGN.Projectiles.Reaper;
using DRGN.Items.Weapons.ReaperWeapons.Hooks;

namespace DRGN.Items.Weapons.Crystil
{
    public class CrystilHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 85;
            item.useTime = 30;
            item.knockBack = 10f;
            item.value = 100000;
            item.rare = ItemRarityID.Red;
            item.shoot = mod.ProjectileType("CrystilHook");
            item.shootSpeed = 15f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CrystilBar"), 11);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}