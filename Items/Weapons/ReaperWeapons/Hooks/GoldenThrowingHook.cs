

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
    public class GoldenThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 9;
            item.useTime = 26;
            item.knockBack = 2f;
            item.value = 6000;
            item.rare = ItemRarityID.Blue;
            item.shoot = mod.ProjectileType("GoldenHook");
            item.shootSpeed = 9f;

        }       
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.GoldBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);

            recipe2.AddIngredient(ItemID.PlatinumBar, 15);
            recipe2.AddTile(TileID.Anvils);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }



    }
}