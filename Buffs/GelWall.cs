﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace DRGN.Buffs
{
    public class GelWall : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Gel Wall");
            Description.SetDefault("Blocks hostile projectiles");
            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;



        }









    }
}
