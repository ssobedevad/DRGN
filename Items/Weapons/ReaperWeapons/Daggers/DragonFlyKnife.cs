

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
    public class DragonFlyKnife : ThrowingDagger
    {
        public override void SSD()
        {
            item.damage = 125;
            BloodHuntRange = 200;
            item.useTime = 22;
            item.value = 850000;          
            item.rare = ItemRarities.DarkBlue;
            item.shoot = mod.ProjectileType("LunarShank");
            shoot2 = mod.ProjectileType("LunarShankThrown");
            item.knockBack = 6f;
            item.shootSpeed = 12.5f;
        }       
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonFlyDust"), 14);
            recipe.AddIngredient(mod.ItemType("DragonFlyWing"), 14);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"), 6);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}