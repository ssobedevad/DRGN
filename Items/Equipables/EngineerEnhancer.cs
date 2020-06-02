using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Items.EngineerClass;

namespace DRGN.Items.Equipables
{
    
    public class EngineerEnhancer : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Engineer Enhancer");
            Tooltip.SetDefault("Decreases engineer weapon spread"+"\nIncreases fire rate and damage for engineer weapons by 12%"+"\n12% increased engineer crit chance"+"\n20 additional max bullets and 30% reduced reload time");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 30;
            item.value = 100000;
            item.rare = 12;
            item.accessory = true;

        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            player.GetModPlayer<EngineerPlayer>().fireRate *= 1.12f;
            player.GetModPlayer<EngineerPlayer>().spread *= 0.85f;
            player.GetModPlayer<EngineerPlayer>().engineerDamageMult *= 1.12f;
            player.GetModPlayer<EngineerPlayer>().engineerCrit += 12;
            player.GetModPlayer<EngineerPlayer>().MaxBullets2 += 20;
            player.GetModPlayer<EngineerPlayer>().ReloadCounter2 = (int)(player.GetModPlayer<EngineerPlayer>().ReloadCounter2 * 0.70);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar,15);
            recipe.AddIngredient(mod.ItemType("EngineerTalisman"));
            recipe.AddIngredient(mod.ItemType("BulletChain"));
            recipe.AddIngredient(mod.ItemType("EngineerVessel"));
            recipe.AddIngredient(mod.ItemType("LunarConverter"), 5);

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}