

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
    public class Dagger : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 7;
            BloodHuntRange = 100;
            item.useTime = 30;
            item.value = 200;
            item.rare = ItemRarityID.Blue;
            item.shoot = mod.ProjectileType("Dagger");
            shoot2 = mod.ProjectileType("DaggerThrown");
            item.knockBack = 1f;
            item.shootSpeed = 7f;
        }              
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}