
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class LostIcewarriorMagic : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Lost Ice Warrior Hood");
            Tooltip.SetDefault("50% increased magic damage and +100 max mana.");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 1000;
            item.rare = 12;
            item.defense = 5;

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


            player.statManaMax2 += 100;
            player.magicDamage = (float)1.5 * player.magicDamage;

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
