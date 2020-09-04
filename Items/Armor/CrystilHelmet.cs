
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class CrystilHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("20% increased damage"+"\n+1 crystils per explosion");
        }
        public override void SetDefaults()
        {
            item.value = 50000;
            item.rare = ItemRarityID.Purple;
            item.defense = 18;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("CrystilArmor") && legs.type == mod.ItemType("CrystilBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Crystil shards become homing and explode into crystals shards upon impact";
            player.GetModPlayer<DRGNPlayer>().crystilArmorSet = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.allDamage *= 1.2f;
            player.GetModPlayer<DRGNPlayer>().crystalBoost += 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("CrystilBar"), 25);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
