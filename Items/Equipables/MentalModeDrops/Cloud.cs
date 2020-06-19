using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class Cloud : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sun and Moon");
            Tooltip.SetDefault("Increases damage by 25% during the day" + "\nIncreases defense by 30 and max life by 75 at night");
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
            player.GetModPlayer<DRGNPlayer>().clEquip = true;
        }


    }
}