using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class EaterOfWorlds : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormy Headband");
            Tooltip.SetDefault("Crits increase damage by 0.5% up to 100 times but resets upon taking damage");
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
            player.GetModPlayer<DRGNPlayer>().eowEquip = true;
        }


    }
}