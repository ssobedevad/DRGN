using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Reflection;

namespace DRGN.Items.Equipables
{
    public class SoulMagnet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Doubled soul grab range");
        }
        public override void SetDefaults()
        {

            item.value = 15000;
            item.rare = ItemRarityID.Orange;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ReaperPlayer>().SoulPickUpRangeBoost *= 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AshenWood"),12);
            recipe.AddIngredient(mod.ItemType("FlareCrystal"),12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}