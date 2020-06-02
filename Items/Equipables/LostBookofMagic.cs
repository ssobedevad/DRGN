using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{
    
    public class LostBookofMagic : ModItem
    {


        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lost Book of Magic ");
            Tooltip.SetDefault("Autodrink mana potions" + "\n30% increased magic damage" + "\n10% decreased mana usage"+ "\nBeing hit grants restores mana" + "\nExtra long pickup range for mana stars");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 32;
            item.value = 300000;
            item.rare = 12;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaFlower = true;
            player.manaCost *= 0.9f;
            player.manaMagnet = true;
            player.magicCuffs = true;
            player.magicDamage *= 1.3f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DestroyerCuffs"));
            recipe.AddIngredient(mod.ItemType("DestroyerFlower"));
            recipe.AddIngredient(mod.ItemType("CosmoBar"),12);

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}