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
    public class AntWing : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ant wing");
            Tooltip.SetDefault("A wing from an ant");

        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 19;
            item.maxStack = 99;
            item.rare = ItemRarityID.LightRed;
            item.value = 2000;
            
        }

    }
}
