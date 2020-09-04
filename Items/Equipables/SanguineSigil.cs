using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Reflection;
using DRGN.Rarities;

namespace DRGN.Items.Equipables
{

    public class SanguineSigil : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases maximum soul storage by 20" + "\nIncreases bloodhunt range by 125" + "\nIncreases damage per soul by 0.03" + "\n18% increased reaper damage" + "\n2.5x soul grab range");
        }
        public override void SetDefaults()
        {

            item.value = 385000;
            item.rare = ItemRarities.DarkBlue;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ReaperPlayer>().maxSouls2 += 20;
            player.GetModPlayer<ReaperPlayer>().bloodHuntExtraRange += 125;
            player.GetModPlayer<ReaperPlayer>().damageIncPerSoul += 0.03f;
            player.GetModPlayer<ReaperPlayer>().SoulPickUpRangeBoost *= 2.5f;
            player.GetModPlayer<ReaperPlayer>().reaperDamageMult *= 1.18f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SoulCore"));
            recipe.AddIngredient(mod.ItemType("ReaperEmblem"));
            recipe.AddIngredient(mod.ItemType("SoulMagnet"));
            recipe.AddIngredient(mod.ItemType("GalacticEssence"), 6);
            recipe.AddIngredient(ItemID.LunarBar, 10);       
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}