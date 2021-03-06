﻿
using DRGN.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class FireDragonBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fire dragon boots");
            Tooltip.SetDefault("130% increased acceleration and 60% increased wingtime");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 130000;
            item.rare = ItemRarities.FieryOrange;
            item.defense = 28;

        }
        public override void UpdateEquip(Player player)
        {


            player.runAcceleration *= 2.3f;
            player.maxRunSpeed *= 2.3f;
            player.wingTimeMax = (int)(1.6f * player.wingTimeMax);



        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 4);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 6);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
