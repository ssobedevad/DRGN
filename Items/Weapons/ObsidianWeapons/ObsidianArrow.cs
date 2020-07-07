using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Items.Weapons.ObsidianWeapons
{
    public class ObsidianArrow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 12;
            item.ranged = true;
            item.width = 7;
            item.height = 7;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 5.5f;
            item.rare = ItemRarityID.Orange;
            item.value = 450;
            item.shoot = mod.ProjectileType("ObsidianArrow");
            item.ammo = AmmoID.Arrow;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SharpenedObsidian"));

            recipe.AddIngredient(ItemID.IronBar, 1);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();

        }
    }
}