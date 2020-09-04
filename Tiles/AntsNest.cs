using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ObjectData;
namespace DRGN.Tiles
{
    public class AntsNest : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("AntsNest");
            AddMapEntry(new Color(45, 45, 5));


            minPick = 210;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override void RandomUpdate(int i, int j)
        {
            bool q = Main.rand.NextBool(1,25);
            CheckSquare(i-1, j - (q? 2 : 1) , q);
        }

        private void CheckSquare(int startX, int startY, bool Queen)
            {
                bool place = true;
                int height = Queen ? 3 : 2;
                int width = Queen ? 3 : 2;
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        Tile tile = Main.tile[startX + i, startY + j];
                        if (j != height -1)
                        {
                            if (tile.active())
                            {
                                place = false;
                            }
                        }
                        else
                        {
                        
                            if (!tile.active() || tile.halfBrick() || tile.liquid > 0 || !WorldGen.SolidTile(tile))
                            {
                                place = false;
                            }
                        }
                    }
                }
                if (place)
                {             
                    DavesUtils.PlaceModTileXxX(startX + width - 1, startY + height -2,Queen ? (ushort)mod.TileType("QueenAntEgg") : (ushort)mod.TileType("BabyAntEggs"), width, height -1);                           
                }                
            }
        
    }
}


