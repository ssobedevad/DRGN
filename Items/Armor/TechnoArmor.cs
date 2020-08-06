
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class TechnoArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            

            Tooltip.SetDefault("Super life regen" +"\nIncreased jump height and speed");
        }

        public override void SetDefaults()
        {
            
            item.value = 6000;
            item.rare = ItemRarityID.LightPurple;
            item.defense = 20;

        }

        public override void UpdateEquip(Player player)
        {

            player.lifeRegen += 8;
            player.jumpBoost = true;
            player.jumpSpeedBoost *= 1.5f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TechnoBar"), 20);
            
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
