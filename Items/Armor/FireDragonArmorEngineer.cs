
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using DRGN.Items.EngineerClass;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class FireDragonArmorEngineer : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fire dragon Hardhat");
            Tooltip.SetDefault("55% increased engineer damage and 50 max bullets");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 150000;
            item.rare = ItemRarityID.Red;
            item.defense = 32;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("FireDragonChestplate") && legs.type == mod.ItemType("FireDragonBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Hitting enemies has a chance to produce a fire wall that destroys nearby projectiles";
            player.GetModPlayer<DRGNPlayer>().dragonArmorSet = true;


        }
        public override void UpdateEquip(Player player)
        {

            player.GetModPlayer<EngineerPlayer>().engineerDamageMult *= 1.55f;
            player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 50;



        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DragonScale"), 10);
            recipe.AddIngredient(mod.ItemType("SolariumBar"), 13);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
