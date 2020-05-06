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
    public class IndustrialForgeTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileFrameImportant[Type] = true;
            adjTiles = new int[] { TileID.Furnaces, TileID.AdamantiteForge };
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);
            
        }

        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            // Tweak the frame drawn by x position so tiles next to each other are off-sync and look much more interesting.
            int uniqueAnimationFrame = Main.tileFrame[Type] + i;

            uniqueAnimationFrame = uniqueAnimationFrame % 5;
            frameXOffset = uniqueAnimationFrame *51;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter > 4)
            {
                frame = ++frame % 5;
                frameCounter = 0;
            }
        }
         public override void KillMultiTile(int i, int j , int frameX , int frameY)
         { Item.NewItem(i * 16 ,j * 16,48,48,mod.ItemType("IndustrialForge")); }
    }
}

