
using DRGN.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Void Goggles");
            Tooltip.SetDefault("55% increased summon damage" + "\n+6 max minions" + "\n70% increased minion knockback");

        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 560000;
            item.rare = ItemRarities.VoidPurple;
            item.defense = 25;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("VoidChestplate") && legs.type == mod.ItemType("VoidBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases the effectiveness of the void buff";
            player.GetModPlayer<DRGNPlayer>().voidArmorSet = true;


        }
        public override void UpdateEquip(Player player)
        {


            player.minionDamage *= 1.55f;
            player.maxMinions += 6;
            player.minionKB *= 1.7f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("VoidBar"), 9);
            recipe.AddIngredient(mod.ItemType("VoidSoul"), 20);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
