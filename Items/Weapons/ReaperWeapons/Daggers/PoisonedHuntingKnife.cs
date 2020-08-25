

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
    public class PoisonedHuntingKnife : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 19;
            BloodHuntRange = 130;
            item.useTime = 20;
            item.value = 30000;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("PoisonedHuntingKnife");
            shoot2 = mod.ProjectileType("PoisonedHuntingKnifeThrown");
            item.knockBack = 3f;
            item.shootSpeed = 9f;
        }               
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("EarthenBar"), 10);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}