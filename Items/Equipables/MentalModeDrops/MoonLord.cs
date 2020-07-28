using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Rarities;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class MoonLord : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moon Eye");
            Tooltip.SetDefault("Greatly increases speed, damage and defense" + "\n+2 max minions");
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
            player.GetModPlayer<DRGNPlayer>().mlEquip = true;
        }


    }
}