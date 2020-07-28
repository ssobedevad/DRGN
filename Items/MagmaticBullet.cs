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
    public class MagmaticBullet : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 28;
            item.ranged = true;
            item.width = 7;
            item.height = 7;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 5;
            item.rare = ItemRarities.FieryOrange;
            item.value = 1000;
            item.shoot = mod.ProjectileType("MagmaticBullet");
            item.ammo = AmmoID.Bullet;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 555);
            recipe.AddRecipe();
        }
    }
}