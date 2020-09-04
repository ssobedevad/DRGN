

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
using DRGN.Items.Weapons.ReaperWeapons.Daggers;

namespace DRGN.Items.Weapons.Crystil
{
    public class CrystilDagger : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 100;
            BloodHuntRange = 195;
            item.useTime = 38;
            item.value = 850000;
            item.rare = ItemRarityID.Red;
            item.shoot = mod.ProjectileType("CrystilDagger");
            shoot2 = mod.ProjectileType("CrystilDaggerThrown");
            item.knockBack = 6f;
            item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CrystilBar"), 13);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}