
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class LostIcewarriorBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Lost Ice Warrior boots");
            Tooltip.SetDefault("100% increased run acceleration and 40% increased wing time");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 12;
            item.value = 18000;
            item.rare = ItemRarityID.LightRed;
            item.defense = 15;

        }
        public override void UpdateEquip(Player player)
        {


            player.runAcceleration *= 2f;
            player.maxRunSpeed *= 2f;
            player.wingTimeMax = (int)(1.4f * player.wingTimeMax);


        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlacialShard"), 4);
            recipe.AddIngredient(mod.ItemType("GlacialBar"), 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
