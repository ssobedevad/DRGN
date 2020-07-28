using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Rarities;

namespace DRGN.Items.Equipables
{
    
    public class FlareMedallion : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flare Medallion");
            Tooltip.SetDefault("Grants immunity to the burning debuff");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;
            item.value = 100000;
            item.rare = ItemRarities.FieryOrange;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[ModContent.BuffType<Buffs.Burning>()] = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("MagmaticEssence"),5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}