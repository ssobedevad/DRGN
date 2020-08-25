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
    public class HeartEmblem : ModItem
    {

        public override void SetStaticDefaults()
        {
           
            Tooltip.SetDefault("increases max life by 5.");

        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 26;
            item.maxStack = 99;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.HoldingUp;            
            item.consumable = true;
            item.autoReuse = true;
            item.rare = ItemRarities.DarkBlue;
            item.value = 20000;

        }
        public override bool CanUseItem(Player player)
        {

            
            if ((player.GetModPlayer<DRGNPlayer>().heartEmblem < DRGNPlayer.heartEmblemMax)) { return true; }
            else { return false; }
        }
        public override bool UseItem(Player player)


        {
            player.HealEffect(5, true);
            player.statLifeMax2 += 5;

            player.statLife += 5;
            
            
            player.GetModPlayer<DRGNPlayer>().heartEmblem += 1; 
            
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GalacticEssence"));
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ItemID.LifeFruit);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
