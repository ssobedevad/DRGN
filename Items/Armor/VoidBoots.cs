﻿
using DRGN.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class VoidBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Void boots");
            Tooltip.SetDefault("+ 25 critcal armor penetration" + "\n15% increased critical damage");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 12;
            item.value = 550000;
            item.rare = ItemRarities.VoidPurple;
            item.defense = 32;

        }
        public override void UpdateEquip(Player player)
        {


            player.GetModPlayer<DRGNPlayer>().criticalArmorPen += 25;
            player.GetModPlayer<DRGNPlayer>().criticalDamageMult += 0.1f;


        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(mod.ItemType("VoidBar"), 6);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 10);

            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
