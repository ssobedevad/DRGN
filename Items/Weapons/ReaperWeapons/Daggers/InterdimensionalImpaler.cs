

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
    public class InterdimensionalImpaler : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 200;
            BloodHuntRange = 250;
            item.useTime = 18;
            item.value = 1100000;
            item.rare = ItemRarities.VoidPurple;
            item.shoot = mod.ProjectileType("InterdimensionalImpaler");
            shoot2 = mod.ProjectileType("InterdimensionalImpalerThrown");
            item.shootSpeed = 15f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidBar"), 16);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 16);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}