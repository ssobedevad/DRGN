
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class LostIcewarriorSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Lost Ice Warrior Goggles");
            Tooltip.SetDefault("31% increased summon damage and +2 max minions.");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 20000;
            item.rare = ItemRarityID.LightRed;
            item.defense = 14;

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


            player.maxMinions += 2;
            player.minionDamage *= 1.31f;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlacialShard"), 8);
            recipe.AddIngredient(mod.ItemType("GlacialBar"), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
