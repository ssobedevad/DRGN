
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AntMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("18% increased summon damage." + "\n + 3 max minions");
        }
        public override void SetDefaults()
        {
            item.value = 8500;
            item.rare = ItemRarityID.Orange;
            item.defense = 2;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("AntExoskeleton") && legs.type == mod.ItemType("AntBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Friendly ants create more ants when hitting enemies";
            player.GetModPlayer<DRGNPlayer>().antArmorSet = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage *= 1.18f;
            player.maxMinions += 3;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntEssence"), 8);
            recipe.AddIngredient(mod.ItemType("AntJaw"), 3);
            recipe.AddIngredient(mod.ItemType("FireAntJaw"), 3);
            recipe.AddIngredient(mod.ItemType("ToxicAntJaw"), 3);
            recipe.AddIngredient(mod.ItemType("ElectricAntJaw"), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }



}
