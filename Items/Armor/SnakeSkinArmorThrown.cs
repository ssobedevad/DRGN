
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class SnakeSkinArmorThrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Snakeskin Helmet");
            Tooltip.SetDefault("15% increased thrown damage." + "\n25% increased thrown velocity");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 11;
            item.value = 1000;
            item.rare = ItemRarityID.Blue;
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
            player.thrownDamage *= 1.15f;
            player.thrownVelocity *= 1.25f;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SnakeScale"), 8);
            recipe.AddIngredient(ItemID.Cactus, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }



}
