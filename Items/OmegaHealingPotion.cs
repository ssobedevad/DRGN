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
    public class OmegaHealingPotion : ModItem
    {

        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Grants 3 seconds of immunity on use"+"\nAutomatically consumed if no potion sickness preventing death but granting a longer potion sickness");

        }
        public override void SetDefaults()
        {

            item.maxStack = 99;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.healLife = 450;
            item.consumable = true;
            item.autoReuse = false;
            item.rare = ItemRarities.FieryOrange;
            item.value = 150000;

        }
        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff(BuffID.PotionSickness)) { return false; }

            return true;
        }
        public override bool UseItem(Player player)
        {

            player.immune = true;
            player.AddBuff(BuffID.PotionSickness, 1800);
            player.immuneTime = 180;

            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("MegaHealingPotion"),5);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 10);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 10);

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }
}
