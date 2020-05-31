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
    public class FlariumConverter : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flarium Converter");
            Tooltip.SetDefault("Used to turn insectium materials into flarium ones");

        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = 4;
            item.value = 100;

        }
        

    }
}
