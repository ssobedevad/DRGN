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
    public class Flint : ModItem
    {

        
        public override void SetDefaults()
        {
            
            item.maxStack = 99;
            item.rare = ItemRarityID.Blue;
            item.value = 100;
        }

    }
}
