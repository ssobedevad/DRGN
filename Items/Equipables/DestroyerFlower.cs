using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{
    
    public class DestroyerFlower : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Destroyer Flower");
            Tooltip.SetDefault("Autodrink mana potions" + "\n15% increased magic damage" + "\n10% decreased mana usage");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 44;
            item.value = 25000;
            item.rare = ItemRarityID.LightPurple;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaFlower = true;
            player.manaCost *= 0.9f;
            player.magicDamage *= 1.15f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ManaFlower);
            recipe.AddIngredient(ItemID.DestroyerEmblem);

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}