using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{
    
    public class ToxicMedallion : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toxic Medallion");
            Tooltip.SetDefault("Grants immunity to the melting debuff");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;
            item.value = 100000;
            item.rare = 4;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[ModContent.BuffType<Buffs.Melting>()] = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ToxicFlesh"),10);
            recipe.AddIngredient(ItemID.Cactus, 10);
            recipe.AddIngredient(mod.ItemType("SnakeScale"), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}