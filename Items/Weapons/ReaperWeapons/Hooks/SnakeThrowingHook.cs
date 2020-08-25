

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
using DRGN.Projectiles.Reaper;

namespace DRGN.Items.Weapons.ReaperWeapons.Hooks
{
    public class SnakeThrowingHook : ThrowingHook
    {
        public override void SSD()
        {
            item.damage = 11;
            item.useTime = 28;
            item.knockBack = 2f;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.shoot = mod.ProjectileType("SnakeHook");
            item.shootSpeed = 9.75f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cactus, 15);
            recipe.AddIngredient(mod.ItemType("SnakeScale"), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}