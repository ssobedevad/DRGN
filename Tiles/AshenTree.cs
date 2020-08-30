

//using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.Tiles
{
    public class AshenTree : ModTree
    {
        public override bool CanDropAcorn()
        {
            return false;
        }
        private Mod mod => ModLoader.GetMod("DRGN");

        public override int CreateDust()
        {
            return DustID.Fire;
        }
        public override int DropWood()
        {
            return ItemType<Items.AshenWood>();
        }
        public override Texture2D GetTexture()
        {
            return mod.GetTexture("Tiles/AshenTree");
        }

        public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
        {
            frame = (int)DRGNModWorld.FlamingTreeFrame;
            int dustid = Dust.NewDust(new Microsoft.Xna.Framework.Vector2(i * 16 - 8, (j - 10) * 16), 16, 160, DustID.Fire , 0, -10);
            Main.dust[dustid].noGravity = true;
            return mod.GetTexture("Tiles/AshenTree_Tops");
        }

        public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
        {
            frame = (int)DRGNModWorld.FlamingTreeFrame;
            int dustid = Dust.NewDust(new Microsoft.Xna.Framework.Vector2(i * 16 - 8, (j - 10) * 16), 16, 160, DustID.Fire, 0, -10);
            Main.dust[dustid].noGravity = true;
            return mod.GetTexture("Tiles/AshenTree_Branches");
        }

    }
}
