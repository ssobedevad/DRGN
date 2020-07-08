using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Items
{
    public class ElectricAntJaw : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electric ant jaw");
            Tooltip.SetDefault("A jaw from a shockingly scary ant");

        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.rare = ItemRarityID.Orange;
            
            item.value = 4000;
        }

    }
}
