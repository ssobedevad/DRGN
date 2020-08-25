

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
using DRGN.Projectiles.Reaper.Scythes;

namespace DRGN.Items.Weapons.ReaperWeapons.Scythes
{
    public class Scythe : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 9;
            item.knockBack = 2f;
            item.value = 200;
            item.rare = ItemRarityID.Blue;
            item.useTime = 30;
            item.shootSpeed = 6f;
            item.shoot = mod.ProjectileType("Scythe");
            shoot2 = mod.ProjectileType("ScytheThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}