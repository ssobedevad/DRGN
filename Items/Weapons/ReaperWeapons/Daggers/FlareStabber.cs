

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
    public class FlareStabber : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 150;
            BloodHuntRange = 220;
            item.useTime = 20;
            item.value = 950000;
            item.rare = ItemRarities.FieryOrange;
            item.shoot = mod.ProjectileType("FlareStabber");
            shoot2 = mod.ProjectileType("FlareStabberThrown");
            item.shootSpeed = 13.5f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 17);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 17);

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}