

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
    public class AntShank : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 25;
            BloodHuntRange = 140;
            item.useTime = 30;
            item.value = 80000;
            item.rare = ItemRarityID.Orange;
            item.shoot = mod.ProjectileType("AntShank");
            shoot2 = mod.ProjectileType("AntShankThrown");
            item.knockBack = 3.5f;
            item.shootSpeed = 9.5f;
        }       
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntJaw"), 10);
            recipe.AddIngredient(mod.ItemType("AntEssence"), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}