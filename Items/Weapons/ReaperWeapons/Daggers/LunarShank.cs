

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
    public class LunarShank : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 100;
            BloodHuntRange = 190;
            item.useTime = 25;
            item.value = 750000;
            item.rare = ItemRarityID.Red;
            item.shoot = mod.ProjectileType("LunarShank");
            shoot2 = mod.ProjectileType("LunarShankThrown");
            item.knockBack = 6f;
            item.shootSpeed = 12f;
        }        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LunarFragment"), 18);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}