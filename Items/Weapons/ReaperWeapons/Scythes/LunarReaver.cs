

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
    public class LunarReaver : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 110;
            item.knockBack = 10.5f;
            item.value = 1000000;
            item.rare = ItemRarityID.Red;
            item.useTime = 21;
            item.shootSpeed = 9.5f;
            item.shoot = mod.ProjectileType("LunarReaver");
            shoot2 = mod.ProjectileType("LunarReaverThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LunarFragment"), 18);
            recipe.AddIngredient(ItemID.LunarBar, 18);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}