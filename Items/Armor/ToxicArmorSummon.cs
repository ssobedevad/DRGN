
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ToxicArmorSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Toxic Goggles");
            Tooltip.SetDefault("28% increased summon damage." + "\n+1 max minions" + "\n50% increased minion knocback");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 2200;
            item.rare = ItemRarityID.Green;
            item.defense = 6;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ToxicArmor") && legs.type == mod.ItemType("ToxicArmorBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Produces toxic bubbles that melt enemies";
            player.GetModPlayer<DRGNPlayer>().toxicArmorSet = true;


        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage *= 1.28f;
            player.maxMinions += 1;
            player.minionKB *= 1.5f;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"), 14);
            recipe.AddIngredient(mod.ItemType("EarthenBar"), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }



}
