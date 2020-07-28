using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Tiles;
using DRGN.Rarities;

namespace DRGN.Items.Equipables
{
    
    public class GalactiteYoyoBag : ModItem
    {


        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("Grants Mastery of all things yoyo");
        }

        public override void SetDefaults()
        {
            
            item.value = 1000000;
            item.rare = ItemRarities.GalacticRainbow;
            item.accessory = true;

        }
        public override void UpdateEquip(Player player)
        {
            player.yoyoGlove = true;
            player.yoyoString = true;
            player.counterWeight = 556 + Main.rand.Next(6);
            player.GetModPlayer<DRGNPlayer>().SuperYoyoBag = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.YoyoBag);
            recipe.AddIngredient(mod.ItemType("GalacticaBar"),10);

            recipe.AddTile(ModContent.TileType<InterGalacticAnvilTile>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}