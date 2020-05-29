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
    public class VoidScrew : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Screw");
            Tooltip.SetDefault("Void Screw");

        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.maxStack = 999;
            item.rare = 4;
            item.value = 100;

        }

    }
}
