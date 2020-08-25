
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
    public class ShadowDemonDagger : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 12;
            BloodHuntRange = 115;
            item.useTime = 24;
            item.value = 8000;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("ShadowDemonDagger");
            shoot2 = mod.ProjectileType("ShadowDemonDaggerThrown");
            item.knockBack = 2f;
            item.shootSpeed = 8f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShadowBar"), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}