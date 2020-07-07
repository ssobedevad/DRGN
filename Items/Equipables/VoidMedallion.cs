using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{
    
    public class VoidMedallion : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Medallion");
            Tooltip.SetDefault("Void web debuff lasts half a second");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;
            item.value = 10000;
            item.rare = ItemRarityID.Purple;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DRGNPlayer>().voidDebuffReduced = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("VoidSoul"),40);
            
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}