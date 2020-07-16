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
    public class MegaHealingPotion : ModItem
    {

        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Grants 1 second of immunity on use");

        }
        public override void SetDefaults()
        {
            
            item.maxStack = 99;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.healLife = 250;
            item.consumable = true;
            item.autoReuse = false;
            item.rare = ItemRarityID.Red;
            item.value = 50000;

        }
        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff(BuffID.PotionSickness)) { return false; }

            return true;
        }
        public override bool UseItem(Player player)
        {

            player.immune = true;

            player.immuneTime = 60;

            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"));
            recipe.AddIngredient(ItemID.SuperHealingPotion,5);
            
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this,5);
            recipe.AddRecipe();
        }
    }
}
