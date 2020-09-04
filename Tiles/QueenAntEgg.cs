using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
namespace DRGN.Tiles
{
    public class QueenAntEgg : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(200, 200, 100));
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return !NPC.AnyNPCs(mod.NPCType("QueenAnt"));
        }
        public override bool CanExplode(int i, int j)
        {
            return !NPC.AnyNPCs(mod.NPCType("QueenAnt"));
        }        
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        { 
            NPC.NewNPC(i * 16, j * 16, mod.NPCType("QueenAnt")); 
        }
    }
}

