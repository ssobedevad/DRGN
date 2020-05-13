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
    public class VoidBrickTileArena : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;
            
            
            AddMapEntry(new Color(30, 5, 30));
            minPick = 225;
        }
        public override void NearbyEffects(int i,int j,bool closer)
        { if  (NPC.AnyNPCs(mod.NPCType("VoidSnakeHead")) == false) { Main.tile[i, j].active(false); } }
    }
}


