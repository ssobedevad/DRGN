

using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Projectiles;
using DRGN.Tiles;
using DRGN.Rarities;
using DRGN.Items.Weapons.ReaperWeapons.Scythes;

namespace DRGN.Items.Weapons.Crystil
{
    public class CrystilScythe : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 70;
            item.knockBack = 10.5f;
            item.value = 1200000;
            item.rare = ItemRarityID.Red;
            item.useTime = 45;
            item.shootSpeed = 9.5f;
            item.shoot = mod.ProjectileType("CrystilScythe");
            shoot2 = mod.ProjectileType("CrystilScytheThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CrystilBar"), 12);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}