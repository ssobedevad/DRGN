
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class CloudWarriorMelee : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Cloud Warrior Headgear");
            Tooltip.SetDefault("33% increased melee damage and +45 max health");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 50000;
            item.rare = ItemRarityID.Yellow;
            item.defense = 38;

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


            player.statLifeMax2 += 45;
            player.meleeDamage *= 1.33f;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 6);
            recipe.AddIngredient(ItemID.FragmentVortex, 6);
            recipe.AddIngredient(ItemID.FragmentNebula, 6);
            recipe.AddIngredient(ItemID.FragmentStardust, 6);
            recipe.AddIngredient(mod.ItemType("CosmoBar"), 15);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
