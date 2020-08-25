

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
    public class SnakeSkinStabber : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 14;
            BloodHuntRange = 120;
            item.useTime = 22;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("SnakeSkinStabber");
            shoot2 = mod.ProjectileType("SnakeSkinStabberThrown");
            item.knockBack = 2.5f;
            item.shootSpeed = 8.5f;
        }        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cactus, 10);
            recipe.AddIngredient(mod.ItemType("SnakeScale"), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}