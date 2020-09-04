using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using DRGN.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN
{
    public class Swarm 
    {
        //Initializing an Array that can be used in any file

        private static int SwarmSize;
        //Setup for an Invasion After setting up
        public static void StartSwarm()
        {
            //Set to no invasion if one is present
            if (Main.invasionType != 0 && Main.invasionSize == 0)
            {
                Main.invasionType = 0;
            }

            //Once it is set to no invasion setup the invasion
            if (Main.invasionType == 0)
            {
                //Checks amount of players for scaling
                int numPlayers = 0;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active)
                    {
                        numPlayers++;
                    }
                }
                if (numPlayers > 0)
                {
                    SwarmSize = 40;
                    //Invasion setup
                    Main.invasionType = -1; //Not going to be using an invasion that is positive since those are vanilla invasions
                    DRGNModWorld.SwarmUp = true;
                    if (DRGNModWorld.downedQueenAnt) { SwarmSize = 75; }
                    if (NPC.downedMechBossAny) { SwarmSize = 125; }
                    if (NPC.downedMoonlord) { SwarmSize = 185; }
                
                    Main.invasionSize = SwarmSize * numPlayers;
                    Main.invasionSizeStart = Main.invasionSize;
                    Main.invasionProgress = 0;
                    Main.invasionProgressIcon = 0 + 3;
                    Main.invasionProgressWave = 0;
                    Main.invasionProgressMax = Main.invasionSizeStart;
                    Main.invasionWarn = 100; //This doesn't really matter, as this does not work, I like to keep it here anyways
                    
                        Main.invasionX = (double)Main.spawnTileX; //Starts invasion immediately rather than wait for it to spawn
                        return;
                    
                   //Set the initial starting location of the invasion to max tiles
                }
            }
        }

        //Text for messages and syncing
        public static void SwarmWarning()
        {
            String text = "";
            if (Main.invasionX == (double)Main.spawnTileX)
            {
                text = "The swarm has arrived!";
            }
            if (Main.invasionSize <= 0)
            {
                text = "The swarm has subsided";
                DRGNModWorld.SwarmKilled = true;                
            }
            if (Main.netMode == 0)
            {
                Main.NewText(text, 175, 75, 255, false);
                return;
            }
           
        }

        //Updating the invasion
        public static void UpdateSwarm()
        {
            //If the custom invasion is up
            if (DRGNModWorld.SwarmUp)
            {
                //End invasion if size less or equal to 0
                if (Main.invasionSize <= 0)
                {
                    DRGNModWorld.SwarmUp = false;
                    SwarmWarning();
                    Main.invasionType = 0;
                    Main.invasionDelay = 0;
                }
                if ( Main.invasionSize <= 20) 
                { 
                    if (Main.netMode != NetmodeID.MultiplayerClient) 
                        
                    {
                        if (NPC.downedMoonlord && !NPC.AnyNPCs(ModContent.NPCType<NPCs.Boss.DragonFly>()))
                        {
                            NPC.SpawnOnPlayer(Player.FindClosest(new Vector2(Main.maxTilesX / 2, Main.maxTilesY / 2), 1, 1), ModContent.NPCType<NPCs.Boss.DragonFly>());
                        }
                        else if (DRGNModWorld.downedQueenAnt && !NPC.AnyNPCs(ModContent.NPCType<NPCs.Boss.QueenAnt>()))
                        {
                            NPC.SpawnOnPlayer(Player.FindClosest(new Vector2(Main.maxTilesX / 2, Main.maxTilesY / 2), 1, 1), ModContent.NPCType<NPCs.Boss.QueenAnt>());
                        }
                    } 
                }

                //Do not do the rest if invasion already at spawn
                if (Main.invasionX == (double)Main.spawnTileX)
                {
                    return;
                }

                //Update when the invasion gets to Spawn
                float moveRate = (float)Main.dayRate;

                //If the invasion is greater than the spawn position
                if (Main.invasionX > (double)Main.spawnTileX)
                {
                    //Decrement invasion x as to "move them"
                    Main.invasionX -= (double)moveRate;

                    //If less than the spawn pos, set invasion pos to spawn pos and warn players that invaders are at spawn
                    if (Main.invasionX <= (double)Main.spawnTileX)
                    {
                        Main.invasionX = (double)Main.spawnTileX;
                        SwarmWarning();
                    }
                    else
                    {
                        Main.invasionWarn--;
                    }
                }
                else
                {
                    //Same thing as the if statement above, just it is from the other side
                    if (Main.invasionX < (double)Main.spawnTileX)
                    {
                        Main.invasionX += (double)moveRate;
                        if (Main.invasionX >= (double)Main.spawnTileX)
                        {
                            Main.invasionX = (double)Main.spawnTileX;
                            SwarmWarning();
                        }
                        else
                        {
                            Main.invasionWarn--;
                        }
                    }
                }
            }
        }

        //Checks the players' progress in invasion
        public static void CheckSwarmProgress()
        {
            //Not really sure what this is
            if (Main.invasionProgressMode != 2)
            {
                Main.invasionProgressNearInvasion = false;
                return;
            }

            //Checks if NPCs are in the spawn area to set the flag, which I do not know what it does
            bool flag = false;
            Player player = Main.player[Main.myPlayer];
            Rectangle rectangle = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
            int num = 5000;
            int icon = 0;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active)
                {
                    icon = 0;
                    int type = Main.npc[i].type;
                    for (int n = 0; n < DRGNModWorld.AntTypesAvaliable.Length; n++)
                    {
                        if (type == DRGNModWorld.AntTypesAvaliable[n])
                        {
                            Rectangle value = new Rectangle((int)(Main.npc[i].position.X + (float)(Main.npc[i].width / 2)) - num, (int)(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2)) - num, num * 2, num * 2);
                            if (rectangle.Intersects(value))
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                }
            }
            Main.invasionProgressNearInvasion = flag;
            int progressMax3 = 1;

            //If the custom invasion is up, set the max progress as the initial invasion size
            if (DRGNModWorld.SwarmUp)
            {
                progressMax3 = Main.invasionSizeStart;
            }

            //If the custom invasion is up and the enemies are at the spawn pos
            if (DRGNModWorld.SwarmUp && (Main.invasionX == (double)Main.spawnTileX))
            {
                //Shows the UI for the invasion
                Main.ReportInvasionProgress(Main.invasionSizeStart - Main.invasionSize, progressMax3, icon, 0);
            }

            //Syncing start of invasion
            foreach (Player p in Main.player)
            {
                NetMessage.SendData(MessageID.InvasionProgressReport, p.whoAmI, -1, null, Main.invasionSizeStart - Main.invasionSize, (float)Main.invasionSizeStart, (float)(Main.invasionType + 3), 0f, 0, 0, 0);
            }
        }
    }
}