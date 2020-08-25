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
    public class ManaEmblem : ModItem
    {

        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("increases max mana by 5.");

        }
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.rare = ItemRarityID.Lime;
            item.consumable = true;
            item.autoReuse = true;
            item.value = 20000;

        }
        public override bool CanUseItem(Player player)
        {


            if ((player.GetModPlayer<DRGNPlayer>().lunarStars < DRGNPlayer.lunarStarsMax)) { return true; }
            else { return false; }
        }
        public override bool UseItem(Player player)
        {
            player.ManaEffect(5);
            player.statManaMax2 += 5;

            player.statManaMax2 += 5;


            player.GetModPlayer<DRGNPlayer>().lunarStars += 1;

            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ManaFruit"));
            recipe.AddIngredient(mod.ItemType("GalacticEssence"));
            recipe.AddIngredient(ItemID.ManaCrystal);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
