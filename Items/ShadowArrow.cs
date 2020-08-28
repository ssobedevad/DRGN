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
    public class ShadowArrow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 3;
            item.ranged = true;
            item.width = 7;
            item.height = 7;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 5;
            item.rare = ItemRarityID.Blue;
            item.value = 1000;
            item.shoot = mod.ProjectileType("ShadowArrow");
            item.ammo = AmmoID.Arrow;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ShadowBar"));
            recipe.AddIngredient(mod.ItemType("ShadowGem"));
            recipe.AddIngredient(ItemID.WoodenArrow, 333);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 333);
            recipe.AddRecipe();

        }
    }
}