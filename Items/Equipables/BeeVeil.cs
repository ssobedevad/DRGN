using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.Neck, EquipType.Back)]
    public class BeeVeil : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bee Veil");
            Tooltip.SetDefault("Being hit grants extra long immunity and releases star bees");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 10000;
            item.rare = ItemRarityID.LightRed;
            item.accessory = true;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeeCloak);
            recipe.AddIngredient(ItemID.StarVeil);
            
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}