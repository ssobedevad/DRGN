using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class Desertserpent : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snake fang");
            Tooltip.SetDefault("Critical strikes have + 10 armor pen and inflict melting");
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
            player.GetModPlayer<DRGNPlayer>().dsEquip = true;
        }


    }
}