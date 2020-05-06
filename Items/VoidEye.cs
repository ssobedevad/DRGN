using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace DRGN.Items
{
    public class VoidEye : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Eye");
            Tooltip.SetDefault("Drives time and pace to part"
            +"\n Single use");
        }
        public override void SetDefaults()
        {
            item.height =16;
            item.width = 32;
            
            item.useTime = 25;
            item.useAnimation = 25;
            item.consumable = true;
            item.useStyle = 3;
            item.maxStack = 1;
        }
        public override bool CanUseItem(Player player)
        {

            
            bool alreadySpawned = (DRGNModWorld.VoidBiome);
            return (!alreadySpawned);
        }
        public override bool UseItem(Player player)

        {
            Main.NewText("The barrier between time and space has ruptured", 60, 5, 60);
            if (DRGNModWorld.VoidBiome != true)
            {
               
                for (int i = 0; i < Main.maxTilesX / 800; i++)       //900 is how many biomes. the bigger is the number = less biomes
                {
                    int X = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
                    int Y = WorldGen.genRand.Next((int)WorldGen.worldSurface, (int)WorldGen.worldSurface + 200);
                    int TileType = mod.TileType("VoidBrickTile");     //this is the tile u want to use for the biome , if u want to use a vanilla tile then its int TileType = 56; 56 is obsidian block

                    WorldGen.TileRunner(X, Y, 150, WorldGen.genRand.Next(50, 250), TileType, false, 0f, 0f, false, true);


                    for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                    {

                        int Xo = X + Main.rand.Next(-240, 240);
                        int Yo = Y + Main.rand.Next(-240, 240);
                        if ((Xo >= 0 || Xo <= Main.maxTilesX) && (Yo >= 0 || Xo <= Main.maxTilesY))
                        {
                            if (Main.tile[Xo, Yo].type == mod.TileType("VoidBrickTile"))   //this is the tile where the ore will spawn
                            {

                                {
                                    WorldGen.TileRunner(Xo, Yo, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), mod.TileType("VoidStoneTile"), false, 0f, 0f, false, true);

                                }
                            }
                        }
                    }
                }
                DRGNModWorld.VoidBiome = true;
            }
            return true;



        }
       
    }
}
