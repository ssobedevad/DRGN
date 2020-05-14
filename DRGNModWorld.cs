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

namespace DRGN
{
    public class DRGNModWorld : ModWorld
    {
        public static bool VoidBiome;
        public static bool EarthenOre;
        public static bool GlacialOre;
        public static bool LuminiteOre;
        public static bool SolariumOre;

        public static bool downedSerpent;
        public static bool downedToxicFrog;
        public static bool downedIceFish;
        public static bool downedCloud;
        public static bool downedDragon;
        public static bool downedVoidSnake;

        public static bool starStorm;
        public override void Initialize()
        {
            VoidBiome = false;
            EarthenOre = false;
            GlacialOre = false;
            LuminiteOre = false;
            SolariumOre = false;

            downedSerpent = false;
            downedToxicFrog = false;
            downedIceFish = false;
            downedCloud = false;
            downedDragon = false;
            downedVoidSnake = false;

        }
        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedDragon = downed.Contains("FireDragon");
            downedSerpent = downed.Contains("DesertSerpent");
            downedToxicFrog = downed.Contains("ToxicFrog");
            downedIceFish = downed.Contains("IceFish");
            downedVoidSnake = downed.Contains("VoidSnake");
            downedCloud = downed.Contains("Cloud");

            var Ores = tag.GetList<string>("ores");
            VoidBiome = Ores.Contains("VoidBiome");
            EarthenOre = Ores.Contains("Earthen");
            GlacialOre = Ores.Contains("Glacial");
            LuminiteOre = Ores.Contains("Luminite");
            SolariumOre = Ores.Contains("Solarium");
        }
        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedDragon) { downed.Add("FireDragon"); }
            if (downedSerpent) { downed.Add("DesertSerpent"); }
            if (downedIceFish) { downed.Add("IceFish"); }
            if (downedVoidSnake) { downed.Add("VoidSnake"); }
            if (downedToxicFrog) { downed.Add("ToxicFrog"); }
            if (downedCloud) { downed.Add("Cloud"); }
            var Ores = new List<string>();
            if (VoidBiome) { Ores.Add("VoidBiome"); }
            if (EarthenOre) { Ores.Add("Earthen"); }
            if (GlacialOre) { Ores.Add("Glacial"); }
            if (LuminiteOre) { Ores.Add("Luminite"); }
            if (SolariumOre) { Ores.Add("Solarium"); }

            return new TagCompound
            {
                ["downed"] = downed,
                ["ores"] = Ores,

            };

        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedDragon = flags[0];
                downedSerpent = flags[1];
                downedIceFish = flags[2];
                downedVoidSnake = flags[3];
                downedToxicFrog = flags[4];
                downedCloud = flags[5];
                BitsByte flags2 = reader.ReadByte();
                VoidBiome = flags2[0];
                EarthenOre = flags2[1];
                GlacialOre = flags2[2];
                LuminiteOre = flags2[3];
                SolariumOre = flags2[4];

            }
            else
            {
                mod.Logger.WarnFormat("DRGN: Unknown loadVersion: {0}", loadVersion);
            }
        }
        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = downedDragon;
            flags[1] = downedSerpent;
            flags[2] = downedIceFish;
            flags[3] = downedVoidSnake;
            flags[4] = downedToxicFrog;
            flags[5] = downedCloud;
            writer.Write(flags);
            var flags2 = new BitsByte();
            flags2[0] = VoidBiome;
            flags2[1] = EarthenOre;
            flags2[2] = GlacialOre;
            flags2[3] = LuminiteOre;
            flags2[4] = SolariumOre;

            writer.Write(flags2);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedDragon = flags[0];
            downedSerpent = flags[1];
            downedIceFish = flags[2];
            downedVoidSnake = flags[3];
            downedToxicFrog = flags[4];
            downedCloud = flags[5];
            BitsByte flags2 = reader.ReadByte();
            VoidBiome = flags2[0];
            EarthenOre = flags2[1];
            GlacialOre = flags2[2];
            LuminiteOre = flags2[3];
            SolariumOre = flags2[4];
        }


        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {






            int genIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Altars"));
            if (genIndex2 == -1)
            {
                return;
            }
            tasks.Insert(genIndex2 + 1, new PassLegacy("Dragon's den", delegate (GenerationProgress progress)
            {
                progress.Message = "Building The Dragon's Den";
                int x = (int)(Main.maxTilesX) - 250;// position of top left of arena
                int y = (int)(Main.maxTilesY) - 220;
                //int x = (int)(player.Center.X) / 16 - 118;
                //int y = (int)(player.Center.Y) / 16 - 69;

                for (int j = 36; j < 105; j++)
                {
                    for (int i = 59; i < 178; i++)// clear empty space

                    {

                        Main.tile[x + i, y + j].active(false);
                        WorldGen.KillWall(x + i, y + j);
                    }
                }
                for (int i = 0; i <= 238; i++)//top line
                {
                    for (int j = 0; j <= 30; j++)// top chunk
                    {
                        if (Main.rand.Next(0, 2) == 1)
                        {
                            Main.tile[x + i, y + j].active(true);
                            Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                        }
                    }
                    if (i > 57 && i < 179)
                    {
                        for (int j = 31; j <= 36; j++)// top chunk
                        {


                            Main.tile[x + i, y + j].active(true);
                            Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");

                        }
                    }
                    else
                    {
                        for (int j = 31; j <= 36; j++)// top chunk
                        {
                            if (Main.rand.Next(0, 2) == 1)
                            {
                                Main.tile[x + i, y + j].active(true);
                                Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                            }
                        }
                    }
                }

                for (int j = 36; j < 79; j++)// loop for each row i is column j is row
                {

                    for (int i = 1; i < 25; i++)
                    {
                        if (Main.rand.Next(0, 4) == 1)// left passive
                        {
                            Main.tile[x + i, y + j].active(true);
                            Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                        }


                    }
                    for (int i = 26; i < 53; i++)
                    {
                        if (Main.rand.Next(0, 2) == 1)// left passive
                        {
                            Main.tile[x + i, y + j].active(true);
                            Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                        }


                    }
                    for (int i = 54; i < 59; i++)// left active
                    {


                        Main.tile[x + i, y + j].active(true);
                        Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");



                    }
                    for (int i = 211; i < 236; i++)
                    {
                        if (Main.rand.Next(0, 4) == 1)//right passive
                        {
                            Main.tile[x + i, y + j].active(true);
                            Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                        }
                    }

                    for (int i = 183; i < 210; i++)
                    {
                        if (Main.rand.Next(0, 2) == 1)//right passive
                        {
                            Main.tile[x + i, y + j].active(true);
                            Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                        }
                    }
                    for (int i = 176; i < 182; i++)//right active
                    {


                        Main.tile[x + i, y + j].active(true);
                        Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");



                    }
                }
                for (int j = 86; j < 138; j++)// loop for each row i is column j is row
                {

                    for (int i = 1; i < 25; i++)
                    {
                        if (Main.rand.Next(0, 4) == 1)// left passive
                        {
                            Main.tile[x + i, y + j].active(true);
                            Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                        }


                    }
                    for (int i = 26; i < 53; i++)
                    {
                        if (Main.rand.Next(0, 2) == 1)// left passive
                        {
                            Main.tile[x + i, y + j].active(true);
                            Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                        }


                    }
                    for (int i = 54; i < 59; i++)// left active
                    {


                        Main.tile[x + i, y + j].active(true);
                        Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");



                    }
                    for (int i = 211; i < 236; i++)
                    {
                        if (Main.rand.Next(0, 4) == 1)//right passive
                        {
                            Main.tile[x + i, y + j].active(true);
                            Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                        }
                    }

                    for (int i = 183; i < 210; i++)
                    {
                        if (Main.rand.Next(0, 2) == 1)//right passive
                        {
                            Main.tile[x + i, y + j].active(true);
                            Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                        }
                    }
                    for (int i = 176; i < 182; i++)//right active
                    {


                        Main.tile[x + i, y + j].active(true);
                        Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");



                    }
                }

                for (int j = 86; j < 91; j++)
                {
                    for (int i = 0; i < 87; i++)//platform bottomleft
                    {
                        Main.tile[x + i, y + j].active(true);
                        Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                    }
                    for (int i = 148; i < 236; i++)//platform bottomright
                    {
                        Main.tile[x + i, y + j].active(true);
                        Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                    }
                }
                for (int j = 60; j < 66; j++)
                {
                    for (int i = 97; i < 138; i++)//platform middle top
                    {
                        Main.tile[x + i, y + j].active(true);
                        Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                    }
                }




                for (int i = 0; i <= 236; i++)// bottom row
                {
                    for (int j = 115; j <= 138; j++)// bottom chunk
                    {
                        if (Main.rand.Next(0, 2) == 1)
                        {
                            Main.tile[x + i, y + j].active(true);
                            Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                        }
                    }
                    if (i > 57 && i < 179)
                    {
                        for (int j = 105; j <= 114; j++)// bottom chunk
                        {

                            Main.tile[x + i, y + j].active(true);
                            Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");

                        }
                    }
                    else
                    {
                        for (int j = 105; j <= 114; j++)// bottom chunk
                        {
                            if (Main.rand.Next(0, 2) == 1)
                            {
                                Main.tile[x + i, y + j].active(true);
                                Main.tile[x + i, y + j].type = (ushort)mod.TileType("DragonBrick");
                            }
                        }
                    }
                }

            }));
        }












        public static int isVoidBiome = 0;
        public static int DragonDen = 0;

        public override void TileCountsAvailable(int[] tileCounts)
        {
            DragonDen = tileCounts[mod.TileType("DragonBrick")];
            isVoidBiome = tileCounts[mod.TileType("VoidBrickTile")];    //this make the public static int customBiome counts as customtileblock
        }
        public override void PreUpdate()
        {
            if (!Main.dayTime && NPC.downedMoonlord)
            {
                if (starStorm && Main.rand.Next(0, 20) == 1) { Projectile.NewProjectile(new Vector2(Main.rand.Next(0, Main.maxTilesX * 16), (int)(Main.worldSurface * 1.35)), new Vector2(0, Main.rand.Next(1, 5)), mod.ProjectileType("LunarStar"), 1000, 0f, 0); } 
                else if (Main.rand.Next(0, 1000) == 1) { Projectile.NewProjectile(new Vector2(Main.rand.Next(0, Main.maxTilesX * 16), (int)(Main.worldSurface * 1.35)), new Vector2(0, Main.rand.Next(1, 5)), mod.ProjectileType("LunarStar"), 1000, 0f, 0); }
            }
            if (starStorm && Main.rand.Next(0, 20) == 1) { Projectile.NewProjectile(new Vector2(Main.rand.Next(0, Main.maxTilesX * 16), (int)(Main.worldSurface * 1.35)), new Vector2(0, Main.rand.Next(20, 50)), ProjectileID.FallingStar, 1000, 0f, 0); }
        
                if (!Main.dayTime && Main.time == 0.0 && Main.rand.Next(0, 10) == 1) { Main.NewText("The sky is filled with stars",200,20,200); starStorm = true; }

            if (starStorm && Main.dayTime) { starStorm = false; }
        }

    }
}



