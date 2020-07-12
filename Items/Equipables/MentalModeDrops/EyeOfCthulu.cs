using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class EyeOfCthulu : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("All seeing eye");
            Tooltip.SetDefault("15% increased critical strike chance and greatly increased vision");
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
            player.GetModPlayer<DRGNPlayer>().eocEquip = true;
        }


    }
}