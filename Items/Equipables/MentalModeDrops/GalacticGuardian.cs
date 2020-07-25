using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class GalacticGuardian : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactic Eye");
            Tooltip.SetDefault("Immune to contact damage from non-boss npcs and always kills non-boss npcs in 1 hit");
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
            player.GetModPlayer<DRGNPlayer>().ggEquip = true;
        }


    }
}