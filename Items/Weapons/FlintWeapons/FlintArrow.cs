using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Items.Weapons.FlintWeapons
{
    public class FlintArrow : ModItem
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
            item.rare = ItemRarityID.Blue;
            item.value = 300;
            item.shoot = mod.ProjectileType("FlintArrow");
            item.ammo = AmmoID.Arrow;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Flint"));
            
            recipe.AddIngredient(ItemID.Wood, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();

        }
    }
}