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
    public class AntJaw : ModItem
    {

        public override void SetStaticDefaults()
        {
            
            Tooltip.SetDefault("A jaw from an ant");

        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = ItemRarityID.Green;
            item.value = 1000;
        }

    }
}
