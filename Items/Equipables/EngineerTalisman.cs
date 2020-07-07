using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Items.EngineerClass;

namespace DRGN.Items.Equipables
{
    
    public class EngineerTalisman : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Engineer Tailsman");
            Tooltip.SetDefault("Increases engineer damage and crit by 10%");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;
            item.value = 10000;
            item.rare = ItemRarityID.LightRed;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            player.GetModPlayer<EngineerPlayer>().engineerDamageMult *= 1.1f;
            player.GetModPlayer<EngineerPlayer>().engineerCrit += 10;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar,12);
            recipe.AddIngredient(ItemID.Shackle);
            recipe.AddIngredient(mod.ItemType("GoldenConverter"),5);
            
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}