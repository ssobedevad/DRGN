

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
    public class CosmoSlash : ThrowingScythe
    {
        public override void SSD()
        {
            item.damage = 82;
            item.knockBack = 9f;
            item.value = 350000;
            item.rare = ItemRarityID.Cyan;
            item.useTime = 22;
            item.shootSpeed = 9.5f;
            item.shoot = mod.ProjectileType("CosmoSlash");
            shoot2 = mod.ProjectileType("CosmoSlashThrown");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CosmoBar"), 16);
            recipe.AddIngredient(ItemID.SpectreBar, 16);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}