using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables.MentalModeDrops
{

    public class DragonFly : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Dragonfly");
            Tooltip.SetDefault("Grants unlimited wingtime"+"\nMagic quiver effect and frostburn effect on hitting enemies");
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
            player.GetModPlayer<DRGNPlayer>().dfEquip = true;
        }


    }
}