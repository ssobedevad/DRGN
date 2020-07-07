using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{
    
    public class DestroyerCuffs : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Destroyer Cuffs");
            Tooltip.SetDefault("Being hit grants restores mana"+"\n15% increased magic damage"+"\nExtra long pickup range for mana stars");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 34;
            item.value = 25000;
            item.rare = ItemRarityID.LightPurple;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaMagnet = true;
            player.magicCuffs = true;
            player.magicDamage *= 1.15f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CelestialEmblem);
            recipe.AddIngredient(ItemID.CelestialCuffs);
            
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}