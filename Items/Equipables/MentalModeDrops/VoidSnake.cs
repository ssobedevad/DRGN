using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Rarities;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class VoidSnake : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void portal");
            Tooltip.SetDefault("Doubles the power of the void buff effect"+"\nCrits apply void buff to enemies");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 10000;
            item.rare = ItemRarities.Mental;

            item.accessory = true;

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<DRGNPlayer>().vsEquip = true;
        }


    }
}