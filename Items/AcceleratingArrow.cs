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
            recipe.AddRecipeGroup("DRGN:T3HmB", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 222);
            recipe.AddRecipe();
            
        }
    }
}