

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
namespace DRGN.Items.Weapons.ReaperWeapons.Scythes
{
    public class GoldenScythe : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 12;
            item.knockBack = 2.5f;
            item.value = 1000;
            item.rare = ItemRarityID.Blue;
            item.useTime = 27;
            item.shootSpeed = 6.25f;
            item.shoot = mod.ProjectileType("GoldenScythe");
            shoot2 = mod.ProjectileType("GoldenScytheThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.GoldBar, 13);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);

            recipe2.AddIngredient(ItemID.PlatinumBar, 13);
            recipe2.AddTile(TileID.Anvils);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }


    }
}