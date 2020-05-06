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
    public class ToxicFlesh : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toxic flesh");
            Tooltip.SetDefault("Skin from a toxic being");

        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.rare = 4;
            item.value = 100;

        }

    }
}
