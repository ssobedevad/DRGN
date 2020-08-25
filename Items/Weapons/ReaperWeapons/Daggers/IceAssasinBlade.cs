

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
    public class IceAssasinBlade : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 38;
            BloodHuntRange = 150;
            item.useTime = 22;
            item.value = 140000;
            item.rare = ItemRarityID.LightRed;
            item.shoot = mod.ProjectileType("IceAssasinBlade");
            shoot2 = mod.ProjectileType("IceAssasinBladeThrown");
            item.knockBack = 4f;
            item.shootSpeed = 10f;
        }             
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlacialBar"), 12);
            recipe.AddIngredient(mod.ItemType("GlacialShard"), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}