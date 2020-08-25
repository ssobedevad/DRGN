

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
    public class RockSlicer : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 66;
            item.knockBack = 10f;
            item.value = 180000;
            item.rare = ItemRarityID.Lime;
            item.useTime = 35;
            item.shootSpeed = 9f;
            item.shoot = mod.ProjectileType("RockSlicer");
            shoot2 = mod.ProjectileType("RockSlicerThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LihzahrdBar"), 16);

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}