using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Generation;

namespace DRGN.Items
{
    public class AntKey : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ant Key");
            Tooltip.SetDefault("Opens a locked ant chest");

        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.rare = 8;
            item.value = 1000;
            
        }
       

    }
}
