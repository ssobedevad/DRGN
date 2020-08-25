

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
    public class CosmoDagger : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 86;
            BloodHuntRange = 180;
            item.useTime = 28;
            item.value = 550000;
            item.rare = ItemRarityID.Cyan;
            item.shoot = mod.ProjectileType("CosmoDagger");
            shoot2 = mod.ProjectileType("CosmoDaggerThrown");
            item.knockBack = 5.5f;
            item.shootSpeed = 11.5f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CosmoBar"), 12);
            recipe.AddIngredient(ItemID.SpectreBar, 12);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}