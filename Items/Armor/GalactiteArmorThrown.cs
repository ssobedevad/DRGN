﻿
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GalactiteArmorThrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Galactite Mask");
            Tooltip.SetDefault("110% increased throwing velocity" + "\n85% increased throwing damage");

        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 1000;
            item.rare = 12;
            item.defense = 55;

        }
        public override void UpdateEquip(Player player)
        {


            player.thrownVelocity = (float)2.1 * player.thrownVelocity;
            player.thrownDamage = (float)1.85 * player.thrownDamage;


        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidMask"));
            recipe.AddIngredient(mod.ItemType("LostIcewarriorHelmet"));
            recipe.AddIngredient(mod.ItemType("FireDragonArmorThrown"));
            recipe.AddIngredient(mod.ItemType("ToxicArmorThrown"));
            recipe.AddIngredient(mod.ItemType("SnakeSkinArmorThrown"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
