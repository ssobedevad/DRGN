

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
    public class GalactiteThrowingAxe : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 240;
            item.knockBack = 12f;
            item.value = 1200000;
            item.rare = ItemRarities.GalacticRainbow;
            item.useTime = 20;
            item.shootSpeed = 16f;
            item.shoot = mod.ProjectileType("GalactiteThrowingAxe");
            shoot2 = mod.ProjectileType("GalactiteThrowingAxeThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidScythe"));
            recipe.AddIngredient(mod.ItemType("FlareReaver"));
            recipe.AddIngredient(mod.ItemType("DragonFlySlasher"));
            recipe.AddIngredient(mod.ItemType("LunarReaver"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}