﻿

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Wall
{
    public class AntWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = false;


            AddMapEntry(new Color(25, 25, 2));
        }

        
        
    }
}
