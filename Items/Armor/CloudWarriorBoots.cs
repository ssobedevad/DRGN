
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class CloudWarriorBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Cloud Warrior boots");
            Tooltip.SetDefault("150% increased run acceleration and 60% increased wing time");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 12;
            item.value = 45000;
            item.rare = ItemRarityID.Yellow;
            item.defense = 20;

        }
        public override void UpdateEquip(Player player)
        {


            player.runAcceleration *= 2.5f;
            player.maxRunSpeed *= 2.5f;
            player.wingTimeMax = (int)(1.6f * player.wingTimeMax);


        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 3);
            recipe.AddIngredient(ItemID.FragmentVortex, 3);
            recipe.AddIngredient(ItemID.FragmentNebula, 3);
            recipe.AddIngredient(ItemID.FragmentStardust, 3);
            recipe.AddIngredient(mod.ItemType("CosmoBar"), 10);
            recipe.AddTile(mod.TileType("InterGalacticAnvilTile"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
