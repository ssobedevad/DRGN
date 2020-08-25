

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
    public class DragonFlySlasher : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 135;
            item.knockBack = 11.5f;
            item.value = 520000;
            item.rare = ItemRarities.DarkBlue;
            item.useTime = 23;
            item.shootSpeed = 10.25f;
            item.shoot = mod.ProjectileType("DragonFlySlasher");
            shoot2 = mod.ProjectileType("DragonFlySlasherThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonFlyDust"), 16);
            recipe.AddIngredient(mod.ItemType("DragonFlyWing"), 16);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"), 8);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}