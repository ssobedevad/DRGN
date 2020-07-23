using Microsoft.Xna.Framework;

using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.Tiles
{
    public class DRGNGlobalTile : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (type == TileID.Stone && !fail)

            {
                if (Main.rand.Next(50) == 1)
                {
                    Item.NewItem(new Vector2(i * 16, j * 16), mod.ItemType("Flint"));
                }

            }
        }
        public override void SetDefaults()
        {
            Main.tileSpelunker[TileID.LunarOre] = true;
            TileID.Sets.Ore[TileID.LunarOre] = true;
        }
    }

}