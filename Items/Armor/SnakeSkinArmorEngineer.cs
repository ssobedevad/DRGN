
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using DRGN.Items.EngineerClass;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class SnakeSkinArmorEngineer : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Snakeskin Hardhat");
            Tooltip.SetDefault("13% increased Engineer damage." + "\n5 additional max bullets");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 1000;
            item.rare = 2;
            item.defense = 3;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("SnakeSkinBreastplate") && legs.type == mod.ItemType("SnakeSkinBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Provides immunity to posion and melting debuffs and grants increased defense while in the jungle biome";
            player.GetModPlayer<DRGNPlayer>().snakeArmorSet = true;


        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<EngineerPlayer>().engineerDamageMult *= 1.13f;
            player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 5;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SnakeScale"), 9);
            recipe.AddIngredient(ItemID.Cactus, 17);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }



}
