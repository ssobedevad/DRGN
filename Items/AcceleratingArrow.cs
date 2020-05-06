using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Items
{
    public class AcceleratingArrow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 8;
            item.ranged = true;
            item.width = 7;
            item.height = 7;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 5;
            item.rare = 6;
            item.shoot = mod.ProjectileType("AcceleratingArrow");
            item.ammo = AmmoID.Arrow;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteBar,1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 222);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.TitaniumBar, 1);
            recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.SetResult(this, 222);
            recipe2.AddRecipe();
        }
    }
}