

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class TechnoBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            
            Tooltip.SetDefault("115% increased speed and acceleration" + "\n4% increased crit chance");
        }

        public override void SetDefaults()
        {
            
            item.value = 4800;
            item.rare = ItemRarityID.LightPurple;
            item.defense = 12;

        }
        public override void UpdateEquip(Player player)
        {

            player.runAcceleration *= 2.15f;
            player.maxRunSpeed *= 2.15f;
            player.magicCrit += 4;
            player.meleeCrit += 4;
            player.thrownCrit += 4;
            player.rangedCrit += 4;
            player.GetModPlayer<ReaperPlayer>().reaperCrit += 4;

        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(mod.ItemType("TechnoBar"), 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
