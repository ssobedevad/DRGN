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
    public class MetalloidConverter : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metalloid Converter");
            Tooltip.SetDefault("Used to turn wooden materials into metal ones");

        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = ItemRarityID.Green;
            item.value = 100;

        }
        

    }
}
