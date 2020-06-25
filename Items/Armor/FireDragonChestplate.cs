﻿
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using DRGN.Items.EngineerClass;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class FireDragonChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fire dragon chestplate");
            Tooltip.SetDefault("10% increased crit chance");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 180000;
            item.rare = ItemRarityID.Red;
            item.defense = 55;

        }
       

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 10; 
            player.rangedCrit += 10; 
            player.meleeCrit += 10; 
            player.thrownCrit += 10;
            player.GetModPlayer<EngineerPlayer>().engineerCrit += 10;




        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 8);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 12);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
