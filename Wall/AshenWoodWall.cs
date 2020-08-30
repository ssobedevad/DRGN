

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Wall
{
    public class AshenWoodWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            drop = mod.ItemType("AshenWoodWall");
            AddMapEntry(new Color(12, 10, 10));
        }
    }
}
