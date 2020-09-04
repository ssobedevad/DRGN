
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AshenMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("18% increased magic damage." + "\n + 45 max mana");
        }
        public override void SetDefaults()
        {
            item.value = 7000;
            item.rare = ItemRarityID.Orange;
            item.defense = 3;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("AshenChestplate") && legs.type == mod.ItemType("AshenGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Causes enemies to explode into flare crystals on death";
            player.GetModPlayer<DRGNPlayer>().ashenArmorSet = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage *= 1.18f;
            player.statManaMax2 += 45;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(mod.ItemType("AshenWood"), 15);
            recipe.AddIngredient(mod.ItemType("FlareCrystal"), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }



}
