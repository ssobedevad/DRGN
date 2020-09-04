using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using DRGN.Rarities;

namespace DRGN.Items.Equipables.MentalModeDrops
{
    public class Crystil : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystil Charm");
            Tooltip.SetDefault("Nearby enemies explode into crystils upon death");
        }
        public override void SetDefaults()
        {
            item.value = 10000;
            item.rare = ItemRarities.Mental;
            item.accessory = true;

        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<DRGNPlayer>().cyEquip = true;
        }
    }
}