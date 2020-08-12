using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Reflection;

namespace DRGN.Items.Equipables
{

    public class SoulCore : ModItem
    {


        public override void SetStaticDefaults()
        {

            Tooltip.SetDefault("Increases maximum soul storage by 15" + "\nIncreases bloodhunt range by 75");
        }

        public override void SetDefaults()
        {

            item.value = 85000;
            item.rare = ItemRarityID.LightPurple;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ReaperPlayer>().maxSouls2 += 15;
            player.GetModPlayer<ReaperPlayer>().bloodHuntExtraRange += 75;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SoulContainer"));
            recipe.AddIngredient(mod.ItemType("BloodCore"));
            recipe.AddIngredient(ItemID.SoulofSight , 10);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.SoulofFright, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}