

using Microsoft.Xna.Framework;
using Terraria;

public class TilePos
{
	public int X;
	public int Y;
	public bool Empty;
	public TilePos(bool empty, int x = 0, int y = 0)
	{
		Empty = empty;
		Y = y;
		X = x;
	}
	public void Set(int x, int y)
	{
		Empty = false;
		Y = y;
		X = x;
	}
	public void Nullify()
	{
		Empty = true;
	}
	public Vector2 GetVector2Coords()
		=> new Vector2(8 + X * 16, 8 + Y * 16);
	

	public Tile GetTile()
		=> Main.tile[X, Y];
	public TilePos Sum (int XAdd , int YAdd)
    {
		return new TilePos(false, X + XAdd, Y + YAdd);
	}
	public TilePos(Vector2 Position)
	{ 
		Set((int)(Position.X / 16f), (int)(Position.Y / 16f));
	}
	public bool Invalid()
		=> X < 0 || Y < 0 || X > Main.maxTilesX || Y > Main.maxTilesY;

    public override string ToString()
    {
        return $"X: { X } Y: { Y } Empty:  { Empty}";
    }
}
