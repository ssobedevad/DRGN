using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class Destroyer : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mechanical Headband");
            Tooltip.SetDefault("Critting enemies produces a friendly probe to attack with you");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 10000;
            item.expert = true;
            item.accessory = true;

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<DRGNPlayer>().tdEquip = true;
        }


    }
}