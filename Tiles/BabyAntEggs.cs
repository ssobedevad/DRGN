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
    public class BabyAntEggs : ModTile
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
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(100, 100, 50));
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        { 
            NPC.NewNPC(i * 16, j * 16, Main.rand.Next(DRGNModWorld.GetAntTypes()));
            if(!DRGNModWorld.SwarmUp && Main.rand.NextBool(1,100))
            {
                Swarm.StartSwarm();
                Main.NewText("The ground is trembling", 175, 75, 255);
            }                      
        }
    }
}

