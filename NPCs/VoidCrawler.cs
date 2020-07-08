using DRGN.Items.Banners;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DRGN.Tiles;

namespace DRGN.NPCs
{
    public class VoidCrawler : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 5;
        }

        public override void SetDefaults()
        {

            npc.width = 58;               //npc is where you put the npc sprite width.     important
            npc.height = 32;              //npc is where you put the npc sprite height.   important
            npc.damage = 450;
            npc.defense = 50;
            npc.lifeMax = 8500;
            npc.value = 50000;
            npc.aiStyle = -1;
            npc.knockBackResist = 0.4f;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.HitSound = SoundID.NPCHit29;
            npc.DeathSound = SoundID.NPCDeath12;

            banner = npc.type;
            bannerItem = ModContent.ItemType<VoidCrawlerBanner>();

        }
        public override void AI()
        {
            npc.TargetClosest(true);
            bool StillorHit = false;
            if (npc.velocity.X == 0f)
            {
                StillorHit = true;
            }
            if (npc.justHit)
            {
                StillorHit = false;
            }

            int num18 = 60;


            bool flag4 = true;

            flag4 = false;

            float num24 = 1.5f;


            if (npc.velocity.X < num24 && npc.direction == 1)

            {

                npc.velocity.X = npc.velocity.X + 0.12f;

                if (npc.velocity.X > num24)

                {

                    npc.velocity.X = num24;

                }
            }
            else if (npc.velocity.X > -num24 && npc.direction == -1)
            {
                npc.velocity.X = npc.velocity.X - 0.12f;
                if (npc.velocity.X < -num24)
                {
                    npc.velocity.X = -num24;
                }
            }


            int num38 = (int)npc.Center.X / 16;
            int num39 = (int)npc.Center.Y / 16;
            bool flag5 = false;
            for (int l = num38 - 1; l <= num38 + 1; l++)
            {
                for (int m = num39 - 1; m <= num39 + 1; m++)
                {
                    if (Main.tile[l, m].wall > 0)
                    {
                        flag5 = true;
                    }
                }
            }
            if (flag5)
            {
                npc.Transform(mod.NPCType("VoidCrawlerWall"));
            }

            bool flag10 = false;
            if (npc.velocity.Y == 0f)
            {
                int num85 = (int)(npc.position.Y + (float)npc.height + 7f) / 16;
                int num86 = (int)npc.position.X / 16;
                int num87 = (int)(npc.position.X + (float)npc.width) / 16;
                for (int num88 = num86; num88 <= num87; num88++)
                {
                    if (Main.tile[num88, num85] == null)
                    {
                        return;
                    }
                    if (Main.tile[num88, num85].nactive() && Main.tileSolid[(int)Main.tile[num88, num85].type])
                    {
                        flag10 = true;
                        break;
                    }
                }
            }
            if (npc.velocity.Y >= 0f)
            {
                int num89 = 0;
                if (npc.velocity.X < 0f)
                {
                    num89 = -1;
                }
                if (npc.velocity.X > 0f)
                {
                    num89 = 1;
                }
                Vector2 vector10 = npc.position;
                vector10.X += npc.velocity.X;
                int num90 = (int)((vector10.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 1) * num89)) / 16f);
                int num91 = (int)((vector10.Y + (float)npc.height - 1f) / 16f);
                if (Main.tile[num90, num91] == null)
                {
                    Main.tile[num90, num91] = new Tile();
                }
                if (Main.tile[num90, num91 - 1] == null)
                {
                    Main.tile[num90, num91 - 1] = new Tile();
                }
                if (Main.tile[num90, num91 - 2] == null)
                {
                    Main.tile[num90, num91 - 2] = new Tile();
                }
                if (Main.tile[num90, num91 - 3] == null)
                {
                    Main.tile[num90, num91 - 3] = new Tile();
                }
                if (Main.tile[num90, num91 + 1] == null)
                {
                    Main.tile[num90, num91 + 1] = new Tile();
                }
                if (Main.tile[num90 - num89, num91 - 3] == null)
                {
                    Main.tile[num90 - num89, num91 - 3] = new Tile();
                }
                if ((float)(num90 * 16) < vector10.X + (float)npc.width && (float)(num90 * 16 + 16) > vector10.X && ((Main.tile[num90, num91].nactive() && Main.tile[num90, num91].slope() == 0 && Main.tile[num90, num91 - 1].slope() == 0 && Main.tileSolid[(int)Main.tile[num90, num91].type] && !Main.tileSolidTop[(int)Main.tile[num90, num91].type]) || (Main.tile[num90, num91 - 1].halfBrick() && Main.tile[num90, num91 - 1].nactive())) && (!Main.tile[num90, num91 - 1].nactive() || !Main.tileSolid[(int)Main.tile[num90, num91 - 1].type] || Main.tileSolidTop[(int)Main.tile[num90, num91 - 1].type] || (Main.tile[num90, num91 - 1].halfBrick() && (!Main.tile[num90, num91 - 4].nactive() || !Main.tileSolid[(int)Main.tile[num90, num91 - 4].type] || Main.tileSolidTop[(int)Main.tile[num90, num91 - 4].type]))) && (!Main.tile[num90, num91 - 2].nactive() || !Main.tileSolid[(int)Main.tile[num90, num91 - 2].type] || Main.tileSolidTop[(int)Main.tile[num90, num91 - 2].type]) && (!Main.tile[num90, num91 - 3].nactive() || !Main.tileSolid[(int)Main.tile[num90, num91 - 3].type] || Main.tileSolidTop[(int)Main.tile[num90, num91 - 3].type]) && (!Main.tile[num90 - num89, num91 - 3].nactive() || !Main.tileSolid[(int)Main.tile[num90 - num89, num91 - 3].type]))
                {
                    float num92 = (float)(num91 * 16);
                    if (Main.tile[num90, num91].halfBrick())
                    {
                        num92 += 8f;
                    }
                    if (Main.tile[num90, num91 - 1].halfBrick())
                    {
                        num92 -= 8f;
                    }
                    if (num92 < vector10.Y + (float)npc.height)
                    {
                        float num93 = vector10.Y + (float)npc.height - num92;
                        float num94 = 16.1f;

                        num94 += 8f;

                        if (num93 <= num94)
                        {
                            npc.gfxOffY += npc.position.Y + (float)npc.height - num92;
                            npc.position.Y = num92 - (float)npc.height;
                            if (num93 < 9f)
                            {
                                npc.stepSpeed = 1f;
                            }
                            else
                            {
                                npc.stepSpeed = 2f;
                            }
                        }
                    }
                }
            }
            if (flag10)
            {
                int NpcCenterDir = (int)((npc.position.X + (float)(npc.width / 2) + (float)(15 * npc.direction)) / 16f);
                int NpcCenterY = (int)((npc.position.Y + (float)npc.height - 15f) / 16f);

                NpcCenterDir = (int)((npc.position.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 16) * npc.direction)) / 16f);

                if (Main.tile[NpcCenterDir, NpcCenterY] == null)
                {
                    Main.tile[NpcCenterDir, NpcCenterY] = new Tile();
                }
                if (Main.tile[NpcCenterDir, NpcCenterY - 1] == null)
                {
                    Main.tile[NpcCenterDir, NpcCenterY - 1] = new Tile();
                }
                if (Main.tile[NpcCenterDir, NpcCenterY - 2] == null)
                {
                    Main.tile[NpcCenterDir, NpcCenterY - 2] = new Tile();
                }
                if (Main.tile[NpcCenterDir, NpcCenterY - 3] == null)
                {
                    Main.tile[NpcCenterDir, NpcCenterY - 3] = new Tile();
                }
                if (Main.tile[NpcCenterDir, NpcCenterY + 1] == null)
                {
                    Main.tile[NpcCenterDir, NpcCenterY + 1] = new Tile();
                }
                if (Main.tile[NpcCenterDir + npc.direction, NpcCenterY - 1] == null)
                {
                    Main.tile[NpcCenterDir + npc.direction, NpcCenterY - 1] = new Tile();
                }
                if (Main.tile[NpcCenterDir + npc.direction, NpcCenterY + 1] == null)
                {
                    Main.tile[NpcCenterDir + npc.direction, NpcCenterY + 1] = new Tile();
                }
                if (Main.tile[NpcCenterDir - npc.direction, NpcCenterY + 1] == null)
                {
                    Main.tile[NpcCenterDir - npc.direction, NpcCenterY + 1] = new Tile();
                }
                Main.tile[NpcCenterDir, NpcCenterY + 1].halfBrick();
                if (Main.tile[NpcCenterDir, NpcCenterY - 1].nactive() && Main.tile[NpcCenterDir, NpcCenterY - 1].type == 10 && flag4)
                {
                    npc.ai[2] += 1f;
                    npc.ai[3] = 0f;
                    if (npc.ai[2] >= 60f)
                    {

                        npc.velocity.X = 0.5f * (float)(-(float)npc.direction);
                        npc.ai[1] += 5f;

                        npc.ai[2] = 0f;
                        bool flag11 = false;
                        if (npc.ai[1] >= 10f)
                        {
                            flag11 = true;
                            npc.ai[1] = 10f;
                        }
                        WorldGen.KillTile(NpcCenterDir, NpcCenterY - 1, true, false, false);
                        if ((Main.netMode != 1 || !flag11) && flag11 && Main.netMode != 1)
                        {


                            bool flag12 = WorldGen.OpenDoor(NpcCenterDir, NpcCenterY - 1, npc.direction);
                            if (!flag12)
                            {
                                npc.ai[3] = (float)num18;
                                npc.netUpdate = true;
                            }
                            if (Main.netMode == 2 && flag12)
                            {
                                NetMessage.SendData(19, -1, -1, null, 0, (float)NpcCenterDir, (float)(NpcCenterY - 1), (float)npc.direction, 0);
                            }

                        }
                    }
                }
                else
                {
                    if ((npc.velocity.X < 0f && npc.spriteDirection == -1) || (npc.velocity.X > 0f && npc.spriteDirection == 1))
                    {
                        if (Main.tile[NpcCenterDir, NpcCenterY - 2].nactive() && Main.tileSolid[(int)Main.tile[NpcCenterDir, NpcCenterY - 2].type])
                        {
                            if (Main.tile[NpcCenterDir, NpcCenterY - 3].nactive() && Main.tileSolid[(int)Main.tile[NpcCenterDir, NpcCenterY - 3].type])
                            {
                                npc.velocity.Y = -8f;
                                npc.netUpdate = true;
                            }
                            else
                            {
                                npc.velocity.Y = -7f;
                                npc.netUpdate = true;
                            }
                        }
                        else
                        {
                            if (Main.tile[NpcCenterDir, NpcCenterY - 1].nactive() && Main.tileSolid[(int)Main.tile[NpcCenterDir, NpcCenterY - 1].type])
                            {
                                npc.velocity.Y = -6f;
                                npc.netUpdate = true;
                            }
                            else
                            {
                                if (npc.position.Y + (float)npc.height - (float)(NpcCenterY * 16) > 20f && Main.tile[NpcCenterDir, NpcCenterY].nactive() && Main.tile[NpcCenterDir, NpcCenterY].slope() == 0 && Main.tileSolid[(int)Main.tile[NpcCenterDir, NpcCenterY].type])
                                {
                                    npc.velocity.Y = -5f;
                                    npc.netUpdate = true;
                                }
                                else
                                {
                                    if (npc.directionY < 0 && npc.type != 67 && (!Main.tile[NpcCenterDir, NpcCenterY + 1].nactive() || !Main.tileSolid[(int)Main.tile[NpcCenterDir, NpcCenterY + 1].type]) && (!Main.tile[NpcCenterDir + npc.direction, NpcCenterY + 1].nactive() || !Main.tileSolid[(int)Main.tile[NpcCenterDir + npc.direction, NpcCenterY + 1].type]))
                                    {
                                        npc.velocity.Y = -8f;
                                        npc.velocity.X = npc.velocity.X * 1.5f;
                                        npc.netUpdate = true;
                                    }
                                    else
                                    {
                                        if (flag4)
                                        {
                                            npc.ai[1] = 0f;
                                            npc.ai[2] = 0f;
                                        }
                                    }
                                }
                            }
                        }
                        if (npc.velocity.Y == 0f && StillorHit && npc.ai[3] == 1f)
                        {
                            npc.velocity.Y = -5f;
                        }
                    }




                }
            }
            else
            {
                if (flag4)
                {
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                }
            }


        }





        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), mod.ItemType("VoidSilk"), 5);
            if (Main.rand.Next(3) == 0)
            { Item.NewItem(npc.getRect(), mod.ItemType("VoidStone"), 5); }
        }
       
        public override void FindFrame(int frameHeight)
        {

            if (npc.velocity.Y != 0f)
            {
                npc.frameCounter = 0.0;
                if (npc.velocity.Y < 0f)
                {
                    npc.frame.Y = frameHeight * 4;
                }
                else
                {
                npc.frame.Y = 0;
                }

            }
            npc.spriteDirection = npc.direction;
            npc.frameCounter += Math.Abs(npc.velocity.X) * 1.1f;
            if (npc.frameCounter < 6.0)
            {
                npc.frame.Y = frameHeight;
            }
            else if (npc.frameCounter < 12.0)
            {
                npc.frame.Y = frameHeight * 2;
            }
            else if (npc.frameCounter < 18.0)
            {
                npc.frame.Y = frameHeight * 3;
            }
            else
            {
                npc.frameCounter = 0.0;
            }

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedMoonlord == true)
            {
                return (spawnInfo.spawnTileType == ModContent.TileType<VoidBrickTile>() || spawnInfo.spawnTileType == ModContent.TileType<VoidStoneTile>()) ? 0.1f : 0f;
            }
            else { return 0f; }
        }




    }
}
