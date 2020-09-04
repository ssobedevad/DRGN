
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class ToxicArmorBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Toxic Boots");
            Tooltip.SetDefault("35% increased speed and acceleration");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 12;
            item.value = 1800;
            item.rare = ItemRarityID.Green;
            item.defense = 4;
            
        }
        public override void UpdateEquip(Player player)
        {

            player.runAcceleration *= 1.35f;
            player.maxRunSpeed *= 1.35f;

        }
       

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"), 10);
            recipe.AddIngredient(mod.ItemType("EarthenBar"), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
