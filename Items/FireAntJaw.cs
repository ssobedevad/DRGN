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
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.rare = ItemRarityID.Orange;
            item.value = 6000;
        }
    }
}
