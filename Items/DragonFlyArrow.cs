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
    public class DragonFlyArrow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 20;
            item.ranged = true;
            item.width = 7;
            item.height = 7;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 5;
            item.rare = 6;
            item.shoot = mod.ProjectileType("DragonFlyArrow");
            item.ammo = AmmoID.Arrow;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonFlyDust"));
            recipe.AddIngredient(mod.ItemType("AntArrow"), 222);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this, 222);
            recipe.AddRecipe();
            
        }
    }
}