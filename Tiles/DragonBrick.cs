using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
namespace DRGN.Tiles
{
	public class DragonBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("DragonBrick");
            AddMapEntry(new Color(15, 5, 5));


            minPick = 225;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override void RandomUpdate(int i, int j)
        {
			int style = WorldGen.genRand.Next(4);
			int offsetX = 0;
			int offsetY = 0;
			switch (style)
			{
				case 0:
					offsetX = -1;
					break;
				case 1:
					offsetX = 1;
					break;
				default:
					offsetY = ((style != 0) ? 1 : (-1));
					break;
			}
			i += offsetX;
			j += offsetY;
			if (!Main.tile[i, j].active())
			{
				
				int numCrystals = 0;
				int offset = 6;
				for (int k = i - offset; k <= i + offset; k++)
				{
					for (int l = j - offset; l <= j + offset; l++)
					{
						if (Main.tile[k, l].active() && Main.tile[k, l].type == mod.TileType("FlareCrystal"))
						{
							numCrystals++;
						}
					}
				}
				if (numCrystals < 4)
				{
					Tile tile = Main.tile[i, j];
					if (tile == null)
					{
						tile = new Tile();
						Main.tile[i, j] = tile;
					}					
					tile.halfBrick(false);
					tile.active(active: true);
					tile.type = (ushort)mod.TileType("FlareCrystal");
					int frame = 0;
					if (WorldGen.SolidTile(i - 1, j))
					{						
						frame = 2;
					}
					else if (WorldGen.SolidTile(i + 1, j))
                    {
						frame = 3;
					}
					else if (WorldGen.SolidTile(i, j - 1))
					{
						frame = 1;
					}
					tile.frameY = 0;
					tile.frameX = (short)(18 * frame);
					
				}
			}
		}
    }
}


