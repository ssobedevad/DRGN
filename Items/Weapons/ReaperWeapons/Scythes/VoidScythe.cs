

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
    public class VoidScythe : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 190;
            item.knockBack = 9f;
            item.value = 950000;
            item.rare = ItemRarities.VoidPurple;
            item.useTime = 24;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("VoidScythe");
            shoot2 = mod.ProjectileType("VoidScytheThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 18);

            recipe.AddIngredient(mod.ItemType("VoidBar"), 25);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}