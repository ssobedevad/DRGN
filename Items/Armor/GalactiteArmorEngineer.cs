
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using DRGN.Items.EngineerClass;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GalactiteArmorEngineer : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Galactite Hardhat");
            Tooltip.SetDefault("65% increased Engineer damage" + "\n75 extra max bullets");

        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 26;
            item.value = 1000;
            item.rare = 12;
            item.defense = 37;

        }
        public override void UpdateEquip(Player player)
        {


            player.GetModPlayer<EngineerPlayer>().engineerDamageMult *= 1.65f;
            player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 75;


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

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidEngineer")); 
            recipe.AddIngredient(mod.ItemType("CloudWarriorEngineer"));
            recipe.AddIngredient(mod.ItemType("LostIcewarriorEngineer"));
            recipe.AddIngredient(mod.ItemType("FireDragonArmorEngineer"));
            recipe.AddIngredient(mod.ItemType("ToxicArmorEngineer"));
            recipe.AddIngredient(mod.ItemType("SnakeSkinArmorEngineer"));
            recipe.AddIngredient(mod.ItemType("GalacticaBar"),10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
