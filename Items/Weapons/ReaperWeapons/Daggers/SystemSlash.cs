

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
    public class SystemSlash : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 55;
            BloodHuntRange = 160;
            item.useTime = 24;
            item.value = 220000;
            item.rare = ItemRarityID.LightPurple;
            item.shoot = mod.ProjectileType("SystemSlash");
            shoot2 = mod.ProjectileType("SystemSlashThrown");
            item.knockBack = 4.5f;
            item.shootSpeed = 10.5f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TechnoBar"), 12);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}