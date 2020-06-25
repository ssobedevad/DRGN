
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using DRGN.Items.EngineerClass;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class CloudWarriorEngineer : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Cloud Warrior Hardhat");
            Tooltip.SetDefault("45% increased engineer damage and 30 max bullets.");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 50000;
            item.rare = ItemRarityID.Yellow;
            item.defense = 18;

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


            player.GetModPlayer<EngineerPlayer>().engineerDamageMult *= 1.45f;
            player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 30;

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
