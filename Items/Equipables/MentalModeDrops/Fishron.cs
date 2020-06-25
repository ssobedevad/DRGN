using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class Fishron : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sea Charm");
            Tooltip.SetDefault("Allows you to turn back time to your previous position");
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
            player.GetModPlayer<DRGNPlayer>().frEquip = true;
        }


    }
}