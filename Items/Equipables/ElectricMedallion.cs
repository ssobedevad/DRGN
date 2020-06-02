using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{
    
    public class ElectricMedallion : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electric Medallion");
            Tooltip.SetDefault("Grants immunity to the shocked debuff");
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
            player.buffImmune[ModContent.BuffType<Buffs.Shocked>()] = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AntJaw"),10);
            recipe.AddIngredient(ItemID.Wire, 10);
            recipe.AddIngredient(ItemID.Bone, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}