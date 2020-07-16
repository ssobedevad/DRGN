using DRGN.Buffs;
using DRGN.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.NPCs
{


    // This abstract class can be used for non splitting worm type NPC.
    public abstract class VoidSnakeAI : ModNPC
    {
        
        private const int laserDamage = 160;
        
        public bool head;
        public bool tail;
        public int minLength;
        public int maxLength;
        public int headType;
        public int bodyType;
        public int tailType;
        public bool flies = false;
        public bool directional = false;
        public float speed;
        public float turnSpeed;
        public float turnSpeed2 = -1;
        public int reqPlayerDist = 1000;
        public Vector2 playerCenterToCircle = new Vector2(-1, -1);

        public bool halfHealthSpawn;
        public bool tenthHealthSpawn;
        private int phaseCounter;
        private int laserPattern;
        private Vector4[] laserSequence1 = new Vector4[16] { new Vector4(-450, -800, 0, 14), new Vector4(-300, -800, 0, 14), new Vector4(-150, -800, 0, 14), new Vector4(0, -800, 0, 14), new Vector4(150, -800, 0, 14), new Vector4(300, -800, 0, 14), new Vector4(450, -800, 0, 14), new Vector4(-1000, 450, 14, 0), new Vector4(-1000, 300, 14, 0), new Vector4(-1000, 150, 14, 0), new Vector4(-1000, 0, 14, 0), new Vector4(-1000, -150, 14, 0), new Vector4(-1000, -300, 14, 0), new Vector4(-1000, -450, 14, 0), new Vector4(-1000, -1000, 7, 7), new Vector4(1000, -1000, -7, 7) };
        private Vector4[] laserSequence2 = new Vector4[17] { new Vector4(-750, -800, 0, 14), new Vector4(-600, -800, 0, 14), new Vector4(-450, -800, 0, 14), new Vector4(-300, -800, 0, 14), new Vector4(-150, -800, 0, 14), new Vector4(0, -800, 0, 14), new Vector4(150, -800, 0, 14), new Vector4(300, -800, 14, 0), new Vector4(450, -800, 14, 0), new Vector4(600, -800, 14, 0), new Vector4(750, -800, 14, 0), new Vector4(-1300, -1000, 7, 7), new Vector4(1300, -1000, -7, 7), new Vector4(-1000, -1000, 7, 7), new Vector4(1000, -1000, -7, 7), new Vector4(-700, -1000, -7, 7), new Vector4(700, -1000, -7, 7) };
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((int)playerCenterToCircle.X);
            writer.Write((int)playerCenterToCircle.Y);

            writer.Write(halfHealthSpawn);
            writer.Write(tenthHealthSpawn);
            writer.Write(phaseCounter);
            writer.Write(laserPattern);

        }
        private bool PlayerCheck(int CheckType , float maxDist = 0)
        {
            for (int i = 0; i < Main.player.Length; i ++)
            {
                if (Main.player[i].active && !Main.player[i].dead)
                {
                    if (CheckType == 0)
                    {
                        if (Main.player[i].HasBuff(BuffType<Webbed>()))
                        { return true; }
                    }
                    else if (CheckType == 1 && maxDist > 0)
                    {
                        if (Vector2.Distance(npc.Center, Main.player[i].Center) > maxDist) { Main.player[i].AddBuff(BuffType<Webbed>(), (DRGNModWorld.MentalMode ? 90 : Main.expertMode ? 75 : 60)); }

                    }
                    else if (CheckType == 2 && maxDist > 0)
                    { if (Vector2.Distance(playerCenterToCircle, Main.player[i].Center) > maxDist) { Main.player[i].Center = playerCenterToCircle; } }
                }
                    
            }
            return false;


        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            playerCenterToCircle.X = reader.ReadInt32();
            playerCenterToCircle.Y = reader.ReadInt32();

            halfHealthSpawn = reader.ReadBoolean();
            tenthHealthSpawn = reader.ReadBoolean();
            phaseCounter = reader.ReadInt32();
            laserPattern = reader.ReadInt32();


        }
        public override void AI()
        {
            if (npc.localAI[1] == 0f)
            {
                npc.localAI[1] = 1f;
                Init();
                if (turnSpeed2 == -1) { turnSpeed2 = turnSpeed; }
                
            }
            if (npc.ai[3] > 0f)
            {
                npc.realLife = (int)npc.ai[3];
            }
            if (!head && npc.timeLeft < 300)
            {
                npc.timeLeft = 300;
            }
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
            }
            if (Main.player[npc.target].dead && npc.timeLeft > 30 && head)
            {
                npc.timeLeft = 30;
                npc.velocity.Y = 40;
            }
            if (NPC.AnyNPCs(mod.NPCType("VoidEye")) || NPC.AnyNPCs(mod.NPCType("MegaVoidEye"))) { npc.dontTakeDamage = true; } else { npc.dontTakeDamage = false; }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {

                if (head)
                {
                    
                    if (npc.life < npc.lifeMax / 2)
                    {

                        halfHealthSpawn = true;
                    }
                    if (npc.life < npc.lifeMax / 10)
                    {





                        tenthHealthSpawn = true;
                    }
                }
                if (halfHealthSpawn)
                {

                    speed = (DRGNModWorld.MentalMode ? 20 : Main.expertMode ? 17 : 14); turnSpeed2 = (DRGNModWorld.MentalMode ? 0.35f : Main.expertMode ? 0.25f : 0.15f);
                    
                }
                if (tenthHealthSpawn) { speed = (DRGNModWorld.MentalMode ? 26 : Main.expertMode ? 23 : 20); turnSpeed2 = (DRGNModWorld.MentalMode ? 0.45f : Main.expertMode ? 0.35f : 0.25f); }

                if (!tail && npc.ai[0] == 0f)
                {
                    if (head)
                    {
                        npc.ai[3] = (float)npc.whoAmI;
                        npc.realLife = npc.whoAmI;
                        npc.ai[2] = (float)Main.rand.Next(minLength, maxLength + 1);
                        npc.ai[0] = (float)NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), bodyType, npc.whoAmI);
                    }
                    else if (npc.ai[2] > 0f)
                    {
                        npc.ai[0] = (float)NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), npc.type, npc.whoAmI);
                    }
                    else
                    {
                        npc.ai[0] = (float)NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), tailType, npc.whoAmI);
                    }
                    Main.npc[(int)npc.ai[0]].ai[3] = npc.ai[3];
                    Main.npc[(int)npc.ai[0]].realLife = npc.realLife;
                    Main.npc[(int)npc.ai[0]].ai[1] = (float)npc.whoAmI;
                    Main.npc[(int)npc.ai[0]].ai[2] = npc.ai[2] - 1f;
                    npc.netUpdate = true;
                }
                if (!head && (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].type != headType && Main.npc[(int)npc.ai[1]].type != bodyType))
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
                if (!tail && (!Main.npc[(int)npc.ai[0]].active || Main.npc[(int)npc.ai[0]].type != bodyType && Main.npc[(int)npc.ai[0]].type != tailType))
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
                if (!npc.active && Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.StrikeNPC, -1, -1, null, npc.whoAmI, -1f, 0f, 0f, 0, 0, 0);
                }
            }
            if (npc.localAI[2] == 0)
            {
               
                if (head) 
                {
                    int circleDiamter = (DRGNModWorld.MentalMode ? 1200 : Main.expertMode ? 1500 : 1800);
                    if (PlayerCheck(0)) { speed = (DRGNModWorld.MentalMode ? 20 : Main.expertMode ? 17 : 14); turnSpeed2 = (DRGNModWorld.MentalMode ? 0.35f : Main.expertMode ? 0.25f : 0.15f); }
                    if (halfHealthSpawn)
                    {
                        circleDiamter = (DRGNModWorld.MentalMode ? 1000 : Main.expertMode ? 1300 : 1600);
                        if (PlayerCheck(0)) { speed =  (DRGNModWorld.MentalMode ? 26 : Main.expertMode ? 23 : 20); turnSpeed2 = (DRGNModWorld.MentalMode ? 0.45f : Main.expertMode ? 0.35f : 0.25f); }
                    
                        if (playerCenterToCircle == new Vector2(-1, -1))
                        {
                            npc.TargetClosest(true);
                            playerCenterToCircle = Main.player[npc.target].Center;


                        }
                        if (phaseCounter % 60 == 0 && Main.netMode != 1)
                        {
                            int projid = Projectile.NewProjectile(playerCenterToCircle + laserSequence2[laserPattern].XY(), laserSequence2[laserPattern].ZW(), ProjectileType<VoidBeamWarning>(), laserDamage, 0f); laserPattern++; if (laserPattern % 17 == 0) { laserPattern = 0; }
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);


                        }
                        if (tenthHealthSpawn)
                        {
                            circleDiamter = (DRGNModWorld.MentalMode ? 850 : Main.expertMode ? 1150 : 1450);
                            if (PlayerCheck(0)) { speed = (DRGNModWorld.MentalMode ? 32 : Main.expertMode ? 29 : 26); turnSpeed2 = (DRGNModWorld.MentalMode ? 0.6f : Main.expertMode ? 0.45f : 0.35f); }

                        }
                    }
                    
                    
                    for (int i = 0; i < (int)(circleDiamter/4); i++)
                    {
                        int dustid = Dust.NewDust(new Vector2(npc.Center.X + (float)Math.Cos((0.035f * i)) * circleDiamter, npc.Center.Y + (float)Math.Sin((0.035f * i)) * circleDiamter), 1, 1, DustID.PinkFlame);
                        Main.dust[dustid].noGravity = true;
                    }
                    PlayerCheck(1, circleDiamter);
                     
                }

                int num180 = (int)(npc.position.X / 16f) - 1;
                int num181 = (int)((npc.position.X + (float)npc.width) / 16f) + 2;
                int num182 = (int)(npc.position.Y / 16f) - 1;
                int num183 = (int)((npc.position.Y + (float)npc.height) / 16f) + 2;
                if (num180 < 0)
                {
                    num180 = 0;
                }
                if (num181 > Main.maxTilesX)
                {
                    num181 = Main.maxTilesX;
                }
                if (num182 < 0)
                {
                    num182 = 0;
                }
                if (num183 > Main.maxTilesY)
                {
                    num183 = Main.maxTilesY;
                }
                bool collision = flies;
                if (!collision)
                {
                    for (int num184 = num180; num184 < num181; num184++)
                    {
                        for (int num185 = num182; num185 < num183; num185++)
                        {
                            if (Main.tile[num184, num185] != null && (Main.tile[num184, num185].nactive() && (Main.tileSolid[(int)Main.tile[num184, num185].type] || Main.tileSolidTop[(int)Main.tile[num184, num185].type] && Main.tile[num184, num185].frameY == 0) || Main.tile[num184, num185].liquid > 64))
                            {
                                Vector2 vector17;
                                vector17.X = (float)(num184 * 16);
                                vector17.Y = (float)(num185 * 16);
                                if (npc.position.X + (float)npc.width > vector17.X && npc.position.X < vector17.X + 16f && npc.position.Y + (float)npc.height > vector17.Y && npc.position.Y < vector17.Y + 16f)
                                {
                                    collision = true;
                                    if (Main.rand.NextBool(100) && npc.behindTiles && Main.tile[num184, num185].nactive())
                                    {
                                        WorldGen.KillTile(num184, num185, true, true, false);
                                    }
                                    if (Main.netMode != NetmodeID.MultiplayerClient && Main.tile[num184, num185].type == 2)
                                    {
                                        ushort arg_BFCA_0 = Main.tile[num184, num185 - 1].type;
                                    }
                                }
                            }
                        }
                    }
                }
                if (!collision && head)
                {
                    Rectangle rectangle = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
                    int mag = reqPlayerDist;
                    bool flag19 = true;
                    for (int num187 = 0; num187 < 255; num187++)
                    {
                        if (Main.player[num187].active)
                        {
                            if (Main.player[num187].HasBuff(BuffType<Webbed>())) { mag = reqPlayerDist / 10; }
                            Rectangle rectangle2 = new Rectangle((int)Main.player[num187].position.X - mag, (int)Main.player[num187].position.Y - mag, mag * 2, mag * 2);
                            if (rectangle.Intersects(rectangle2))
                            {
                                flag19 = false;
                                break;
                            }
                        }
                    }
                    if (flag19)
                    {
                        collision = true;
                    }
                }
                if (directional)
                {
                    if (npc.velocity.X < 0f)
                    {
                        npc.spriteDirection = 1;
                    }
                    else if (npc.velocity.X > 0f)
                    {
                        npc.spriteDirection = -1;
                    }
                }
                float num188 = speed;
                float num189 = turnSpeed;
                Vector2 vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float num191 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
                float num192 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);
                num191 = (float)((int)(num191 / 16f) * 16);
                num192 = (float)((int)(num192 / 16f) * 16);
                vector18.X = (float)((int)(vector18.X / 16f) * 16);
                vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
                num191 -= vector18.X;
                num192 -= vector18.Y;
                float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                if (npc.ai[1] > 0f && npc.ai[1] < (float)Main.npc.Length)
                {
                    try
                    {
                        vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        num191 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - vector18.X;
                        num192 = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - vector18.Y;
                    }
                    catch
                    {
                    }
                    npc.rotation = (float)System.Math.Atan2((double)num192, (double)num191) + 1.57f;
                    num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                    int num194 = npc.width;
                    num193 = (num193 - (float)num194) / num193;
                    num191 *= num193;
                    num192 *= num193;
                    npc.velocity = Vector2.Zero;
                    npc.position.X = npc.position.X + num191;
                    npc.position.Y = npc.position.Y + num192;
                    if (directional)
                    {
                        if (num191 < 0f)
                        {
                            npc.spriteDirection = 1;
                        }
                        if (num191 > 0f)
                        {
                            npc.spriteDirection = -1;
                        }
                    }
                }
                else
                {
                    turnSpeed = turnSpeed2;
                    if (!collision)
                    {
                        npc.TargetClosest(true);
                        npc.velocity.Y = npc.velocity.Y + 0.11f;
                        if (npc.velocity.Y > num188)
                        {
                            npc.velocity.Y = num188;
                        }
                        if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.4)
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X = npc.velocity.X - num189 * 1.1f;
                            }
                            else
                            {
                                npc.velocity.X = npc.velocity.X + num189 * 1.1f;
                            }
                        }
                        else if (npc.velocity.Y == num188)
                        {
                            if (npc.velocity.X < num191)
                            {
                                npc.velocity.X = npc.velocity.X + num189;
                            }
                            else if (npc.velocity.X > num191)
                            {
                                npc.velocity.X = npc.velocity.X - num189;
                            }
                        }
                        else if (npc.velocity.Y > 4f)
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X = npc.velocity.X + num189 * 0.9f;
                            }
                            else
                            {
                                npc.velocity.X = npc.velocity.X - num189 * 0.9f;
                            }
                        }
                    }
                    else
                    {
                        if (!flies && npc.behindTiles && npc.soundDelay == 0)
                        {
                            float num195 = num193 / 40f;
                            if (num195 < 10f)
                            {
                                num195 = 10f;
                            }
                            if (num195 > 20f)
                            {
                                num195 = 20f;
                            }
                            npc.soundDelay = (int)num195;
                            Main.PlaySound(SoundID.Roar, npc.position, 1);
                        }
                        num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                        float num196 = System.Math.Abs(num191);
                        float num197 = System.Math.Abs(num192);
                        float num198 = num188 / num193;
                        num191 *= num198;
                        num192 *= num198;
                        if (ShouldRun())
                        {
                            bool flag20 = true;
                            for (int num199 = 0; num199 < 255; num199++)
                            {
                                if (Main.player[num199].active && !Main.player[num199].dead && Main.player[num199].ZoneCorrupt)
                                {
                                    flag20 = false;
                                }
                            }
                            if (flag20)
                            {
                                if (Main.netMode != NetmodeID.MultiplayerClient && (double)(npc.position.Y / 16f) > (Main.rockLayer + (double)Main.maxTilesY) / 2.0)
                                {
                                    npc.active = false;
                                    int num200 = (int)npc.ai[0];
                                    while (num200 > 0 && num200 < 200 && Main.npc[num200].active && Main.npc[num200].aiStyle == npc.aiStyle)
                                    {
                                        int num201 = (int)Main.npc[num200].ai[0];
                                        Main.npc[num200].active = false;
                                        npc.life = 0;
                                        if (Main.netMode == NetmodeID.Server)
                                        {
                                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num200, 0f, 0f, 0f, 0, 0, 0);
                                        }
                                        num200 = num201;
                                    }
                                    if (Main.netMode == NetmodeID.Server)
                                    {
                                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                    }
                                }
                                num191 = 0f;
                                num192 = num188;
                            }
                        }
                        bool flag21 = false;

                        if (!flag21)
                        {
                            if (npc.velocity.X > 0f && num191 > 0f || npc.velocity.X < 0f && num191 < 0f || npc.velocity.Y > 0f && num192 > 0f || npc.velocity.Y < 0f && num192 < 0f)
                            {
                                if (npc.velocity.X < num191)
                                {
                                    npc.velocity.X = npc.velocity.X + num189;
                                }
                                else
                                {
                                    if (npc.velocity.X > num191)
                                    {
                                        npc.velocity.X = npc.velocity.X - num189;
                                    }
                                }
                                if (npc.velocity.Y < num192)
                                {
                                    npc.velocity.Y = npc.velocity.Y + num189;
                                }
                                else
                                {
                                    if (npc.velocity.Y > num192)
                                    {
                                        npc.velocity.Y = npc.velocity.Y - num189;
                                    }
                                }
                                if ((double)System.Math.Abs(num192) < (double)num188 * 0.2 && (npc.velocity.X > 0f && num191 < 0f || npc.velocity.X < 0f && num191 > 0f))
                                {
                                    if (npc.velocity.Y > 0f)
                                    {
                                        npc.velocity.Y = npc.velocity.Y + num189 * 2f;
                                    }
                                    else
                                    {
                                        npc.velocity.Y = npc.velocity.Y - num189 * 2f;
                                    }
                                }
                                if ((double)System.Math.Abs(num191) < (double)num188 * 0.2 && (npc.velocity.Y > 0f && num192 < 0f || npc.velocity.Y < 0f && num192 > 0f))
                                {
                                    if (npc.velocity.X > 0f)
                                    {
                                        npc.velocity.X = npc.velocity.X + num189 * 2f;
                                    }
                                    else
                                    {
                                        npc.velocity.X = npc.velocity.X - num189 * 2f;
                                    }
                                }
                            }
                            else
                            {
                                if (num196 > num197)
                                {
                                    if (npc.velocity.X < num191)
                                    {
                                        npc.velocity.X = npc.velocity.X + num189 * 1.1f;
                                    }
                                    else if (npc.velocity.X > num191)
                                    {
                                        npc.velocity.X = npc.velocity.X - num189 * 1.1f;
                                    }
                                    if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.5)
                                    {
                                        if (npc.velocity.Y > 0f)
                                        {
                                            npc.velocity.Y = npc.velocity.Y + num189;
                                        }
                                        else
                                        {
                                            npc.velocity.Y = npc.velocity.Y - num189;
                                        }
                                    }
                                }
                                else
                                {
                                    if (npc.velocity.Y < num192)
                                    {
                                        npc.velocity.Y = npc.velocity.Y + num189 * 1.1f;
                                    }
                                    else if (npc.velocity.Y > num192)
                                    {
                                        npc.velocity.Y = npc.velocity.Y - num189 * 1.1f;
                                    }
                                    if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.5)
                                    {
                                        if (npc.velocity.X > 0f)
                                        {
                                            npc.velocity.X = npc.velocity.X + num189;
                                        }
                                        else
                                        {
                                            npc.velocity.X = npc.velocity.X - num189;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    npc.rotation = (float)System.Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
                    if (head)
                    {
                        if (collision)
                        {
                            if (npc.localAI[0] != 1f)
                            {
                                npc.netUpdate = true;
                            }
                            npc.localAI[0] = 1f;
                        }
                        else
                        {
                            if (npc.localAI[0] != 0f)
                            {
                                npc.netUpdate = true;
                            }
                            npc.localAI[0] = 0f;
                        }
                        if ((npc.velocity.X > 0f && npc.oldVelocity.X < 0f || npc.velocity.X < 0f && npc.oldVelocity.X > 0f || npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f || npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f) && !npc.justHit)
                        {
                            npc.netUpdate = true;
                            return;
                        }
                    }
                }

            }
            else if (npc.localAI[2] == 1)
            {
                
                    if (playerCenterToCircle == new Vector2(-1, -1))
                    {
                        npc.TargetClosest(true);
                        playerCenterToCircle = Main.player[npc.target].Center;
                        if (Main.netMode != NetmodeID.MultiplayerClient && head)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                int npcid = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<VoidEye>(), 0, playerCenterToCircle.X, playerCenterToCircle.Y, i * 1.05f);
                                Main.npc[npcid].localAI[0] = 200 + (i * 45);
                                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcid);
                            }
                        }

                    }
                
                    if (head)
                    {

                        PlayerCheck(2, 500);
                        npc.localAI[3] += 0.03f;
                        
                        if (phaseCounter % 60 == 0 && Main.netMode != 1)
                        {
                            int projid = Projectile.NewProjectile(playerCenterToCircle + laserSequence1[laserPattern].XY(), laserSequence1[laserPattern].ZW(), ProjectileType<VoidBeamWarning>(), laserDamage, 0f); laserPattern++; if (laserPattern % 16 == 0) { laserPattern = 0; }
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                        }
                        Vector2 moveTo = new Vector2(playerCenterToCircle.X + (float)Math.Cos(npc.localAI[3]) * 700, playerCenterToCircle.Y + (float)Math.Sin(npc.localAI[3]) * 700);
                        for (int i = 0; i < 200; i++)
                        {
                            int dustid = Dust.NewDust(new Vector2(playerCenterToCircle.X + (float)Math.Cos(npc.localAI[3] + (0.03f * i)) * 500, playerCenterToCircle.Y + (float)Math.Sin(npc.localAI[3] + (0.03f * i)) * 500), 1, 1, DustID.PinkFlame);
                            Main.dust[dustid].noGravity = true;
                        }

                        moveTo = moveTo - npc.Center;
                        float Mag = (float)Math.Sqrt((moveTo.X * moveTo.X + moveTo.Y * moveTo.Y));
                        moveTo *= (30 / Mag);
                        npc.velocity.X = (npc.velocity.X * 20f + moveTo.X) / 21f;
                        npc.velocity.Y = (npc.velocity.Y * 20f + moveTo.Y) / 21f;


                        npc.rotation = npc.velocity.ToRotation() + 1.57f;

                    }
                    else
                    {
                        float num188 = speed;
                        float num189 = turnSpeed;
                        Vector2 vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        float num191 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
                        float num192 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);
                        num191 = (float)((int)(num191 / 16f) * 16);
                        num192 = (float)((int)(num192 / 16f) * 16);
                        vector18.X = (float)((int)(vector18.X / 16f) * 16);
                        vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
                        num191 -= vector18.X;
                        num192 -= vector18.Y;
                        float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                        if (npc.ai[1] > 0f && npc.ai[1] < (float)Main.npc.Length)
                        {
                            try
                            {
                                vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                                num191 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - vector18.X;
                                num192 = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - vector18.Y;
                            }
                            catch
                            {
                            }
                            npc.rotation = (float)System.Math.Atan2((double)num192, (double)num191) + 1.57f;
                            num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                            int num194 = npc.width;
                            num193 = (num193 - (float)num194) / num193;
                            num191 *= num193;
                            num192 *= num193;
                            npc.velocity = Vector2.Zero;
                            npc.position.X = npc.position.X + num191;
                            npc.position.Y = npc.position.Y + num192;
                            if (directional)
                            {
                                if (num191 < 0f)
                                {
                                    npc.spriteDirection = 1;
                                }
                                if (num191 > 0f)
                                {
                                    npc.spriteDirection = -1;
                                }
                            }
                        }
                    }
                


            }
            if (head)
            {

                phaseCounter += 1;
                bool VoidEyes = (NPC.AnyNPCs(mod.NPCType("VoidEye")) || NPC.AnyNPCs(mod.NPCType("MegaVoidEye")));
                if ((phaseCounter >= 1800 && !VoidEyes))
                {
                    npc.localAI[2] = npc.localAI[2] == 1 ? 0 : 1;
                    playerCenterToCircle = new Vector2(-1, -1);
                    phaseCounter = 0;
                    laserPattern = 0;
                    npc.localAI[3] = 0;
                }

                CustomBehavior();

            }
            if (Main.netMode != 1)
            {
                npc.netUpdate = true;
            }

        }

        public virtual void Init()
        {
        }

        public virtual bool ShouldRun()
        {
            return false;
        }

        public virtual void CustomBehavior()
        {
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return head ? (bool?)null : false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            
            Texture2D SnakeTexture = ModContent.GetTexture(Texture);
            Vector2 vect2 = new Vector2(npc.Center.X - Main.screenPosition.X, npc.position.Y + npc.height / 2 - Main.screenPosition.Y);
            Rectangle rect2 = new Rectangle(0, 0, SnakeTexture.Width, SnakeTexture.Height);
            spriteBatch.Draw(
                   SnakeTexture,
                     vect2, rect2, Color.White, npc.rotation, new Vector2(SnakeTexture.Width / 2, SnakeTexture.Height / 2), 1f, SpriteEffects.None, 0f);
            return false;

        }
    }
}
