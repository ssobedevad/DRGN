
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using System.Linq;
using System;
using DRGN.Items.EngineerClass;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class CloudWarriorArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Cloud Warrior chestplate");
            Tooltip.SetDefault("5% increased crit chance");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 1000;
            item.rare = 2;
            item.defense = 55;

        }
       

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 5;
            player.rangedCrit +=5;
            player.meleeCrit += 5;
            player.thrownCrit += 5;


            player.GetModPlayer<EngineerPlayer>().engineerCrit += 5;




        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar,5);
            recipe.AddIngredient(ItemID.FragmentVortex,5);
            recipe.AddIngredient(ItemID.FragmentNebula,5);
            recipe.AddIngredient(ItemID.FragmentStardust,5);
            recipe.AddIngredient(mod.ItemType("CosmoBar"), 12);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
