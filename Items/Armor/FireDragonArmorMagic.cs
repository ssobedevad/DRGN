﻿
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class FireDragonArmorMagic : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fire dragon hood");
            Tooltip.SetDefault("75% increased magic damage" + "\n+150 maximum mana");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 1000;
            item.rare = 2;
            item.defense = 15;

        }
        public override void UpdateEquip(Player player)
        {

            player.magicDamage = (int)(1.75 * player.magicDamage);
            player.statManaMax2 += 150;
           
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 6);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
