using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Graphics;
using DRGN.Rarities;

namespace DRGN.Items
{
    public class VoidSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Soul");
            Tooltip.SetDefault("A fragment of the Void");
            ItemID.Sets.ItemNoGravity[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));


        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = ItemRarities.VoidPurple;

            item.value = 10000;
            item.height = 26;
            item.width = 16;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidBrick"));
            recipe.AddIngredient(mod.ItemType("VoidSilk"));
            recipe.AddIngredient(mod.ItemType("VoidStone"));
            recipe.AddTile(mod.TileType("IndustrialForgeTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        }
}
