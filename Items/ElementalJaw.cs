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
    public class ElementalJaw : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental jaw");
            Tooltip.SetDefault("A jaw to control the elements");

        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = 8;
            item.value = 1000;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("AntJaw"));

            recipe.AddIngredient(mod.ItemType("ElectricAntJaw"));

            recipe.AddIngredient(mod.ItemType("FireAntJaw"));
            recipe.AddIngredient(ItemID.SoulofLight);
            recipe.AddIngredient(ItemID.SoulofNight);



            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this,2);
            recipe.AddRecipe();
        }
    }
}
