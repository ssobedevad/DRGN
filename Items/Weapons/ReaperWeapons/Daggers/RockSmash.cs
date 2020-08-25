

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
    public class RockSmash : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 65;
            BloodHuntRange = 170;
            item.useTime = 35;
            item.value = 320000;
            item.rare = ItemRarityID.Lime;
            item.shoot = mod.ProjectileType("RockSmash");
            shoot2 = mod.ProjectileType("RockSmashThrown");
            item.knockBack = 5f;
            item.shootSpeed = 11f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LihzahrdBar"), 12);          
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}