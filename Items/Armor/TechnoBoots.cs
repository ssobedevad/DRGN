
using DRGN.Items.EngineerClass;
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
            
            
            Tooltip.SetDefault("65% increased speed and acceleration" + "\n3% increased crit chance");
        }

        public override void SetDefaults()
        {
            
            item.value = 4800;
            item.rare = ItemRarityID.LightPurple;
            item.defense = 10;

        }
        public override void UpdateEquip(Player player)
        {

            player.runAcceleration *= 1.75f;
            player.maxRunSpeed *= 1.75f;
            player.magicCrit += 3;
            player.meleeCrit += 3;
            player.thrownCrit += 3;
            player.rangedCrit += 3;
            player.GetModPlayer<EngineerPlayer>().engineerCrit += 3;

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
