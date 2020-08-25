

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
namespace DRGN.Items.Weapons.ReaperWeapons.Scythes
{
    public class ShadowDemonScythe : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 15;
            item.knockBack = 3f;
            item.value = 1500;
            item.rare = ItemRarityID.Green;
            item.useTime = 23;
            item.shootSpeed = 6.25f;
            item.shoot = mod.ProjectileType("ShadowDemonScythe");
            shoot2 = mod.ProjectileType("ShadowDemonScytheThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShadowBar"), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}