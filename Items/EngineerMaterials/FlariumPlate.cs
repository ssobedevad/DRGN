using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Items.EngineerMaterials
{
    public class FlariumPlate : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flarium plate");
            Tooltip.SetDefault("Flarium plate");

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.maxStack = 999;
            item.rare = 4;
            item.value = 100;

        }

    }
}
