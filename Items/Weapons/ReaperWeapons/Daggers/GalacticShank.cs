

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
    public class GalacticShank : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 300;
            BloodHuntRange = 250;
            item.useTime = 16;
            item.value = 2000000;
            item.rare = ItemRarities.GalacticRainbow;
            item.shoot = mod.ProjectileType("GalacticShank");
            shoot2 = mod.ProjectileType("GalacticShankThrown");
            item.shootSpeed = 17f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("InterdimensionalImpaler"));
            recipe.AddIngredient(mod.ItemType("FlareStabber"));
            recipe.AddIngredient(mod.ItemType("DragonFlyKnife"));
            recipe.AddIngredient(mod.ItemType("LunarShank"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}