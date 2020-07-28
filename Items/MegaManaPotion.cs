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
    public class MegaManaPotion : ModItem
    {

        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Grants a random tier 1 nebula buff");

        }
        public override void SetDefaults()
        {

            item.maxStack = 99;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.healMana = 550;
            item.consumable = true;
            item.autoReuse = false;
            item.rare = ItemRarities.DarkBlue;
            item.value = 50000;

        }
        public override bool CanUseItem(Player player)
        {
            

            return true;
        }
        public override bool UseItem(Player player)
        {
            int[] possibleBuffs = new int[3] { BuffID.NebulaUpDmg1, BuffID.NebulaUpLife1, BuffID.NebulaUpMana1 };
            player.AddBuff(Main.rand.Next(possibleBuffs), 180);
            
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"));
            recipe.AddIngredient(ItemID.SuperManaPotion, 5);

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}
