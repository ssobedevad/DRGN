using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
namespace DRGN.Tiles
{
	public class FlareCrystal : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = false;
			Main.tileMergeDirt[Type] = false;
			Main.tileLighted[Type] = false;
			Main.tileBlockLight[Type] = false;
			Main.tileFrameImportant[Type] = true;
			drop = mod.ItemType("FlareCrystal");
			AddMapEntry(new Color(25, 5, 5));
			minPick = 40;
		}
			
	}
}


