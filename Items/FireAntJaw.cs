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
    public class FireAntJaw : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire ant jaw");
            Tooltip.SetDefault("A jaw from a fire breathing ant??");

        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = 8;
            item.value = 1000;
        }

    }
}
