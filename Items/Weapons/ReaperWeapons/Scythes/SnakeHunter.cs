

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
    public class SnakeHunter : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 15;
            item.knockBack = 3f;
            item.value = 1500;
            item.rare = ItemRarityID.Green;
            item.useTime = 23;
            item.shootSpeed = 7f;
            item.shoot = mod.ProjectileType("SnakeHunter");
            shoot2 = mod.ProjectileType("SnakeHunterThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cactus, 14);
            recipe.AddIngredient(mod.ItemType("SnakeScale"), 14);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }
}