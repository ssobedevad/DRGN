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
			SetModTree(new AshenTree());			
            AddMapEntry(new Color(15, 5, 5));			
			minPick = 225;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
		public int SaplingGrowthType()
		{
			return ModContent.TileType<AshenSapling>();
		}

		public override void RandomUpdate(int i, int j)
        {

			if (Main.rand.NextBool(1, 3))
			{
				int numNearby = 0;
				for (int p = -3; p < 4; p++)
				{
					for (int o = -3; o < 4; o++)
					{ if (Main.tile[i + o, j + p].type == (ushort)SaplingGrowthType() || Main.tile[i + o, j + p].type == 5) { numNearby++; } }
				}
				if (numNearby == 0 && !Main.tile[i,j-1].active())
				{
					WorldGen.Place1x2(i, j - 1, (ushort)SaplingGrowthType(), 0);
				}
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
				int origI = i;
				int origJ = j;
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
						tile.frameY = 0;
						tile.frameX = 0;
						tile.type = (ushort)mod.TileType("FlareCrystal");
						Main.tile[origI, origJ].halfBrick(false);
					}
				}
			}
		}
    }
}


