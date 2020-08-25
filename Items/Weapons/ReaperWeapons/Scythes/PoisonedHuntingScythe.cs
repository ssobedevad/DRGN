

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
    public class PoisonedHuntingScythe : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 23;
            item.knockBack = 4f;
            item.value = 25000;
            item.rare = ItemRarityID.Green;
            item.useTime = 27;
            item.shootSpeed = 7.5f;
            item.shoot = mod.ProjectileType("PoisonedHuntingScythe");
            shoot2 = mod.ProjectileType("PoisonedHuntingScytheThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("EarthenBar"), 14);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"), 14);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}