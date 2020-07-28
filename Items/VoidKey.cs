using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Generation;
using DRGN.Rarities;

namespace DRGN.Items
{
    public class VoidKey : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Key");
            Tooltip.SetDefault("Opens a locked void chest");

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.maxStack = 99;
            item.rare = ItemRarities.VoidPurple;

            item.value = 10000;

        }


    }
}
