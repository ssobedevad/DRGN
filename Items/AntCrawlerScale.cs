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
    public class AntCrawlerScale : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ant Crawler Scale");
            Tooltip.SetDefault("Scale of an antlike snake");

        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 27;
            item.maxStack = 99;
            item.rare = ItemRarityID.Red;
            item.value = 10000;
            

        }

    }
}
