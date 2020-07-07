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
    public class VoidConverter : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Converter");
            Tooltip.SetDefault("Used to turn flarium materials into void ones");

        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.rare = ItemRarityID.Purple;
            item.value = 25000;

        }
        

    }
}
