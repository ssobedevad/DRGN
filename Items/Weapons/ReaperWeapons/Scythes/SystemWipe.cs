

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
    public class SystemWipe : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 54;
            item.knockBack = 6f;
            item.value = 120000;
            item.rare = ItemRarityID.LightPurple;
            item.useTime = 27;
            item.shootSpeed = 8.65f;
            item.shoot = mod.ProjectileType("SystemWipe");
            shoot2 = mod.ProjectileType("SystemWipeThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TechnoBar"), 16);
            
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }
}