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
    public class DragonScale : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon scale");
            Tooltip.SetDefault("Scale of a dragon");

        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = ItemRarityID.Red;
            item.value = 10000;

        }
   
    }
}
