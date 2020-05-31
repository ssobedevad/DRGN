
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class LostIcewarriorHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Lost Ice Warrior Helmet");
            Tooltip.SetDefault("45% increased throwing damage and 60% increased throwing velocity.");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 1000;
            item.rare = 12;
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


            player.thrownVelocity = (float)1.6*player.thrownVelocity;
            player.thrownDamage = (float)1.45 * player.thrownDamage;

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
