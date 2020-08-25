

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
    public class FlareReaver : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 162;
            item.knockBack = 13f;
            item.value = 750000;
            item.rare = ItemRarities.FieryOrange;
            item.useTime = 25;
            item.shootSpeed = 12f;
            item.shoot = mod.ProjectileType("FlareReaver");
            shoot2 = mod.ProjectileType("FlareReaverThrown");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 22);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 22);
            
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}