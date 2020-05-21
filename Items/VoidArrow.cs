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
    public class VoidArrow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 45;
            item.ranged = true;
            item.width = 7;
            item.height = 7;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 5;
            item.rare = 6;
            item.shoot = mod.ProjectileType("VoidArrow");
            item.ammo = AmmoID.Arrow;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidBar"));
            recipe.AddIngredient(mod.ItemType("VoidSoul"));
            recipe.AddIngredient(ItemID.WoodenArrow,333);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 333);
            recipe.AddRecipe();
            
        }
    }
}