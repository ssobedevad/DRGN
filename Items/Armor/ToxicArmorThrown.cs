
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ToxicArmorThrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Toxic Helm");
            Tooltip.SetDefault("35% increased thrown damage." + "\n 35% increased thrown velocity");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = 1000;
            item.rare = 2;
            item.defense = 6;

        }

        public override void UpdateEquip(Player player)
        {
            player.thrownDamage = (float)(1.35 * player.thrownDamage);
            player.thrownVelocity = (float)(1.35 * player.thrownVelocity);
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"), 12);
            recipe.AddIngredient(mod.ItemType("EarthenBar"), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }



}
