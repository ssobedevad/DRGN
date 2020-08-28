using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using DRGN.Rarities;

namespace DRGN.Items
{
    public class ShadeArrow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 5;
            item.ranged = true;
            item.width = 7;
            item.height = 7;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 5;
            item.rare = ItemRarityID.Orange;
            item.value = 2000;
            item.shoot = mod.ProjectileType("ShadeArrow");
            item.ammo = AmmoID.Arrow;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShadowArrow"),999);
            recipe.AddIngredient(mod.ItemType("ShadeCrystal"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 999);
            recipe.AddRecipe();

        }
    }
}