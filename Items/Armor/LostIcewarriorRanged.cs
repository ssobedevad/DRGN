﻿
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class LostIcewarriorRanged : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lost Ice Warrior Visor");
            Tooltip.SetDefault("22% increased ranged damage "+"\n25% chance not to consume ammo.");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 20000;
            item.rare = ItemRarityID.LightRed;
            item.defense = 7;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("LostIcewarriorChestplate") && legs.type == mod.ItemType("LostIcewarriorBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Hitting enemies has a chance to produce an ice shard that rains down from the sky";
            player.GetModPlayer<DRGNPlayer>().glacialArmorSet = true;


        }
        public override void UpdateEquip(Player player)
        {


            player.ammoCost75 = true;
            player.rangedDamage *= 1.22f;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlacialShard"), 6);
            recipe.AddIngredient(mod.ItemType("GlacialBar"), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
