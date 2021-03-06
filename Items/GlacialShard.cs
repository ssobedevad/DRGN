﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace DRGN.Items
{
    public class GlacialShard : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glacial shard");
            Tooltip.SetDefault("A fragment of an Ice creature");

        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = ItemRarityID.LightRed;
            item.value = 1000;
        }

    }
}
