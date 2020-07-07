
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class CloudWarriorSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Cloud Warrior Goggles");
            Tooltip.SetDefault("38% increased summon damage and +3 max minions.");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 50000;
            item.rare = ItemRarityID.Yellow;
            item.defense = 16;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("CloudWarriorArmor") && legs.type == mod.ItemType("CloudWarriorBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "A sun follows you around";
            player.GetModPlayer<DRGNPlayer>().cloudArmorSet = true;


        }
        public override void UpdateEquip(Player player)
        {


            player.maxMinions += 3;
            player.minionDamage *= 1.38f;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 7);
            recipe.AddIngredient(ItemID.FragmentVortex, 7);
            recipe.AddIngredient(ItemID.FragmentNebula, 7);
            recipe.AddIngredient(ItemID.FragmentStardust, 7);
            recipe.AddIngredient(mod.ItemType("CosmoBar"), 16);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
