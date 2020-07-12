
using DRGN.Items.EngineerClass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class TechnoArmorSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Techno Goggles");
            Tooltip.SetDefault("40% increased summon damage" + "\n50% increased minion knockback" + "\n+5 max minions");
        }

        public override void SetDefaults()
        {

            item.value = 3000;
            item.rare = ItemRarityID.LightPurple;
            item.defense = 8;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TechnoArmor") && legs.type == mod.ItemType("TechnoBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Nearby enemies become glitched";
            player.GetModPlayer<DRGNPlayer>().technoArmorSet = true;


        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage *= 1.4f;
            player.minionKB *= 1.5f;
            player.maxMinions += 5;

        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TechnoBar"), 26);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }



}
