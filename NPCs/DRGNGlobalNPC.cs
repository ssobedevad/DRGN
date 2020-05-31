﻿using System.IO;
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
        public static int[] invaders;
           
    public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            //If the custom invasion is up and the invasion has reached the spawn pos
            if (DRGNModWorld.SwarmUp && (Main.invasionX == (double)Main.spawnTileX))
            {
                //Clear pool so that only the stuff you want spawns
                pool.Clear();

                //key = NPC ID | value = spawn weight
                //pool.add(key, value)

                //For every ID inside the invader array in our CustomInvasion file
                foreach (int i in invaders)
                {
                    pool.Add(i, 1f); //Add it to the pool with the same weight of 1
                }
            }
        }

        //Changing the spawn rate
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            //Change spawn stuff if invasion up and invasion at spawn
            if (DRGNModWorld.SwarmUp && (Main.invasionX == (double)Main.spawnTileX))
            {
                spawnRate = 100; //Higher the number, the more spawns
                maxSpawns = 10000; //Max spawns of NPCs depending on NPC value
            }
            if (NPC.downedMoonlord) { spawnRate = (int)(spawnRate * 0.3); maxSpawns = maxSpawns * 3; }
        }

        //Adding to the AI of an NPC
        public override void PostAI(NPC npc)
        {
            //Changes NPCs so they do not despawn when invasion up and invasion at spawn
            if (DRGNModWorld.SwarmUp && (Main.invasionX == (double)Main.spawnTileX))
            {
                npc.timeLeft = 1000;
            }
        }

        
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
            if (!npc.boss && npc.lifeMax > 1 && npc.damage > 0 && !npc.friendly && npc.position.Y > Main.rockLayer * 16.0 && npc.value > 0f && Main.rand.NextBool(Main.expertMode ? 2 : 1, 5))
            {
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneJungle && DRGNModWorld.downedToxicFrog)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("ToxicFlesh"));
                }
            }
            
                
            
                if (DRGNModWorld.SwarmUp)
            {
                //Gets IDs of invaders from CustomInvasion file
                
                    //If npc type equal to invader's ID decrement size to progress invasion
                    if (npc.type == mod.NPCType("Ant") || npc.type == mod.NPCType("FireAnt") || npc.type == mod.NPCType("ElectricAnt") )
                    {
                        Main.invasionSize -= 1;
                    }
                    else if (npc.type == mod.NPCType("FlyingAnt"))
                {
                    Main.invasionSize -= 2;
                }
                else if (npc.type == mod.NPCType("AntCrawlerHead")|| npc.type == mod.NPCType("DragonFlyMini"))
                {
                    Main.invasionSize -= 4;
                }

            }
            if (!npc.boss && npc.lifeMax > 1 && npc.damage > 0 && !npc.friendly && NPC.downedMoonlord && npc.value > 0f && Main.rand.NextBool(Main.expertMode ? 2 : 1, 20))
            {
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneUnderworldHeight)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("MagmaticEssence"));
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneJungle)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("EarthenEssence"));
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneSnow)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("GlacialEssence"));
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneSkyHeight)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("LunarEssence"));
                }
            }

            if (npc.type == NPCID.MoonLordCore && DRGNModWorld.LuminiteOre == false)
            {
                Main.NewText("The Lunar Being has fallen and your world has been blessed", 180, 180, 180);
                Item.NewItem(npc.getRect(), mod.ItemType("LunarBlessing"));
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {
                    
                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 600, Main.maxTilesY - 300);
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

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 200, Main.maxTilesY - 600);
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);


                    DRGNModWorld.EarthenOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), (ushort)mod.TileType("EarthenOre"));




                }
            }
            if (npc.type == mod.NPCType("Cloud") && DRGNModWorld.CosmoOre == false)
            {

                Main.NewText("The sun and moon have condensed", 180, 180, 255);
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 600, Main.maxTilesY - 600);
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);


                    DRGNModWorld.EarthenOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), (ushort)mod.TileType("CosmoOre"));




                }
            }
            if (npc.type == mod.NPCType("VoidSnakeHead") && DRGNModWorld.VoidOre == false)
            {

                Main.NewText("The void has collapsed", 0, 255, 100);
                for (int k = 0; k < 750; k++)                     //750 is the ore spawn rate. the bigger is the number = more ore spawns
                {

                    int Y = Main.rand.Next((int)WorldGen.worldSurface + 700, Main.maxTilesY );
                    int X = Main.rand.Next(100, Main.maxTilesX - 100);


                    DRGNModWorld.VoidOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), (ushort)mod.TileType("VoidOre"));
                    Y = Main.rand.Next((int)WorldGen.worldSurface , Main.maxTilesY);
                    X = Main.rand.Next(100, Main.maxTilesX - 100);
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(10, 15), WorldGen.genRand.Next(10, 15), (ushort)mod.TileType("GalacticaOre"));




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

                    int Y = Main.rand.Next(Main.maxTilesY-400, Main.maxTilesY);
                    int X = Main.rand.Next(10, Main.maxTilesX - 10);


                    
                    DRGNModWorld.SolariumOre = true;
                    WorldGen.OreRunner(X, Y, (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(5, 10), (ushort)mod.TileType("SolariumOre"));




                }

            }
           
        }
    }
}