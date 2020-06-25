
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GalactiteArmorSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Galactite Goggles");
            Tooltip.SetDefault("75% increased summon damage" + "\n+8 max minions" + "\n100% increased minion knockback");

        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 600000;
            item.rare = ItemRarityID.Expert;
            item.defense = 43;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("GalactiteArmor") && legs.type == mod.ItemType("GalactiteBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "A star will protect you";
            player.GetModPlayer<DRGNPlayer>().galactiteArmorSet = true;


        }
        public override void UpdateEquip(Player player)
        {


            player.minionDamage *= 1.75f;
            player.maxMinions += 8;
            player.minionKB *= 2f;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidSummon"));
            recipe.AddIngredient(mod.ItemType("CloudWarriorSummon"));
            recipe.AddIngredient(mod.ItemType("LostIcewarriorSummon"));
            recipe.AddIngredient(mod.ItemType("FireDragonArmorSummon"));
            recipe.AddIngredient(mod.ItemType("ToxicArmorSummon"));
            recipe.AddIngredient(mod.ItemType("SnakeSkinArmorSummon"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
