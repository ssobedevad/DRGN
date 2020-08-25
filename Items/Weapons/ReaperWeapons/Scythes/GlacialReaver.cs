

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
    public class GlacialReaver : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 35;
            item.knockBack = 5.5f;
            item.value = 110000;
            item.rare = ItemRarityID.LightRed;
            item.useTime = 28;
            item.shootSpeed = 8.25f;
            item.shoot = mod.ProjectileType("GlacialReaver");
            shoot2 = mod.ProjectileType("GlacialReaverThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlacialBar"), 16);
            recipe.AddIngredient(mod.ItemType("GlacialShard"), 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}