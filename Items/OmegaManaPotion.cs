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
    public class OmegaManaPotion : ModItem
    {

        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Grants a random tier 3 nebula buff");

        }
        public override void SetDefaults()
        {

            item.maxStack = 99;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.healMana = 850;
            item.consumable = true;
            item.autoReuse = false;
            item.rare = ItemRarities.FieryOrange;
            item.value = 150000;
        }
        public override bool UseItem(Player player)
        {
            int[] possibleBuffs = new int[3] { BuffID.NebulaUpDmg3, BuffID.NebulaUpLife3, BuffID.NebulaUpMana3 };
            player.AddBuff(Main.rand.Next(possibleBuffs), 240);
           
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("MegaManaPotion"), 5);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 10);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }
}
