using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            AddMapEntry(new Color(45, 15, 15));
            minPick = 40;
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            tile.frameY = 0;
            tile.frameX = 0;
            Texture2D text = ModContent.GetTexture("DRGN/Tiles/FlareCrystal");
            int rotation = -1;
            if (WorldGen.SolidTile(i - 1, j))
            {
                rotation = 90;
            }
            else if (WorldGen.SolidTile(i + 1, j))
            {
                rotation = 270;
            }
            else if (WorldGen.SolidTile(i, j - 1))
            {
                rotation = 180;
            }
            else if (WorldGen.SolidTile(i, j + 1))
            {
                rotation = 0;
            }
            if (rotation == -1) { WorldGen.KillTile(i, j); rotation = 0; }
            spriteBatch.Draw(text, new Vector2(i * 16 + 8 - Main.screenPosition.X, j * 16 + 8 - Main.screenPosition.Y) + new Vector2(16 * 12, 16 * 12), null, Lighting.GetColor(i, j), MathHelper.ToRadians(rotation), text.Size() * 0.5f, 1f, 0f, 0f);
            return false;
        }

    }
}


