using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Items.Equipables
{
    [AutoloadEquip(EquipType.Neck, EquipType.Back)]
    public class BeeVeil : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bee Veil");
            Tooltip.SetDefault("Being hit grants extra long immunity and releases star bees");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 100000;
            
            item.accessory = true;

        }


    }
}