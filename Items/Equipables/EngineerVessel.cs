using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Items.EngineerClass;

namespace DRGN.Items.Equipables
{
    
    public class EngineerVessel : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Engineer Vessel");
            Tooltip.SetDefault("Decreases engineer weapon spread and increases fire rate for engineer weapons by 10%");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 30;
            item.value = 40000;
            item.rare = ItemRarityID.Cyan;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            player.GetModPlayer<EngineerPlayer>().fireRate *= 1.1f;
            player.GetModPlayer<EngineerPlayer>().spread *= 0.9f; ;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar,6);
            recipe.AddIngredient(ItemID.EyeoftheGolem);
            recipe.AddIngredient(mod.ItemType("CosmoBar"),5);
            recipe.AddIngredient(mod.ItemType("PlantenConverter"), 5);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}