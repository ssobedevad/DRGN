

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
namespace DRGN.Items.Weapons.ReaperWeapons.Daggers
{
    public class GoldenDagger : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 9;
            BloodHuntRange = 110;
            item.useTime = 25;
            item.value = 6000;
            item.rare = ItemRarityID.Blue;
            item.shoot = mod.ProjectileType("GoldenDagger");
            shoot2 = mod.ProjectileType("GoldenDaggerThrown");
            item.knockBack = 1.5f;
            item.shootSpeed = 7.5f;
        }                                        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.GoldBar, 9);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);

            recipe2.AddIngredient(ItemID.PlatinumBar, 9);
            recipe2.AddTile(TileID.Anvils);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
}