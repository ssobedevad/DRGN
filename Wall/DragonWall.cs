

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Wall
{
    public class DragonWall : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = false;
            AddMapEntry(new Color(35, 10, 10));
            if (!DRGN.wallsForTreeGrow.Contains(Type)) { DRGN.wallsForTreeGrow.Add(Type); }
        }



    }
}
