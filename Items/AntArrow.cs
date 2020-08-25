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
    public class AntArrow : ModItem
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
            item.rare = ItemRarityID.Orange;
            item.value = 800;
            item.shoot = mod.ProjectileType("AntArrow");
            item.ammo = AmmoID.Arrow;
            item.createTile= mod.TileType("ManaFruit");
            item.useStyle = 1;
            item.useTime = 10;
            item.useAnimation = 10;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntJaw"));
            recipe.AddIngredient(mod.ItemType("AntEssence"));
            recipe.AddIngredient(ItemID.WoodenArrow,222);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 222);
            recipe.AddRecipe();
            
        }
    }
}