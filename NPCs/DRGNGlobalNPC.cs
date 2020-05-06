using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using System.Linq;
using Terraria.DataStructures;
using Terraria.Localization;
using System;

namespace DRGN.NPCs
{
    
    class DRGNGlobalNPC : GlobalNPC
    {
      
        public override void NPCLoot(NPC npc)
        {
            // We check several things that filter out bosses and critters, as well as the depth that the npc died at. 
            if (!npc.boss && npc.lifeMax > 1 && npc.damage > 0 && !npc.friendly && npc.position.Y > Main.rockLayer * 16.0 && npc.value > 0f && Main.rand.NextBool(Main.expertMode ? 2 : 1, 30))
            {
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneJungle)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("FrogClaw"));
                }
            }

            if (npc.type == NPCID.MoonLordCore && DRGNModWorld.LuminiteOre == false)
            {
                Main.NewText("The Lunar Being has fallen and your world has been blessed", 180, 180, 180);

                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 400, Main.maxTilesY - 300);
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);

 
                    DRGNModWorld.LuminiteOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), TileID.LunarOre);




                }
                
               
            }
            if (npc.type == mod.NPCType("ToxicFrog") && DRGNModWorld.EarthenOre == false)
            {

                Main.NewText("The earth has released its energy", 0, 255, 100);
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 400, Main.maxTilesY - 300);
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);


                    DRGNModWorld.EarthenOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), (ushort)mod.TileType("EarthenOre"));




                }
            }
            if (npc.type == mod.NPCType("IceFish") && DRGNModWorld.GlacialOre == false)
             {
                Main.NewText("The ancient being has fallen granting your world its power", 0, 100, 255);
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 400, Main.maxTilesY - 300);
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);


                  
                    DRGNModWorld.GlacialOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), (ushort)mod.TileType("GlacialOre"));




                }
            }
            if(npc.type == mod.NPCType("FireDragon") && DRGNModWorld.SolariumOre == false) 
            {
                Main.NewText("Hell has risen releasing power into the depths of your world", 255, 50, 50);
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 400, Main.maxTilesY - 300);
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);


                    
                    DRGNModWorld.SolariumOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), (ushort)mod.TileType("SolariumOre"));




                }

            }
           
        }
    }
}