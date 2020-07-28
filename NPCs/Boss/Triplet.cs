using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace DRGN.NPCs.Boss
{
    [AutoloadBossHead]
    public class Triplet : ModNPC
    {

        private const int IchorShotDamage = 30;
        

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Triplet");
            Main.npcFrameCount[npc.type] = 7;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 20500;
            npc.height = 110;
            npc.width = 110;
            npc.aiStyle = -1;
            npc.damage = 25;
            npc.defense = 12;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.value = 10000;
            npc.knockBackResist = 0f;
            npc.noTileCollide = true;
           
            npc.noGravity = true;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.ai[0] = 0; // part of phase 
            bossBag = ItemID.TwinsBossBag;
            

        }
        

        public override bool Autoload(ref string name)
        {

            base.Autoload(ref name);
            mod.AddBossHeadTexture("DRGN/NPCs/Boss/Triplet_Head_Boss_1");

            return true;
        }


        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 1.6f);
            npc.damage = (int)(npc.damage * 1.3f);
            npc.defense = (int)(npc.defense * 1.3f);
        }
        public override bool PreAI()
        {
            
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest();
            }
            bool PlayerDead = Main.player[npc.target].dead;
            float Xdiff = npc.position.X + (float)(npc.width / 2) - Main.player[npc.target].position.X - (float)(Main.player[npc.target].width / 2);
            float Ydiff = npc.position.Y + (float)npc.height - 59f - Main.player[npc.target].position.Y - (float)(Main.player[npc.target].height / 2);
            float angleToPlayer = (float)Math.Atan2(Ydiff, Xdiff) + 1.57f;
            if (angleToPlayer < 0f)
            {
                angleToPlayer += 6.283f;
            }
            else if ((double)angleToPlayer > 6.283)
            {
                angleToPlayer -= 6.283f;
            }
            float num438 = 0.15f;
            if (npc.rotation < angleToPlayer)
            {
                if ((double)(angleToPlayer - npc.rotation) > 3.1415)
                {
                    npc.rotation -= num438;
                }
                else
                {
                    npc.rotation += num438;
                }
            }
            else if (npc.rotation > angleToPlayer)
            {
                if ((double)(npc.rotation - angleToPlayer) > 3.1415)
                {
                    npc.rotation += num438;
                }
                else
                {
                    npc.rotation -= num438;
                }
            }
            if (npc.rotation > angleToPlayer - num438 && npc.rotation < angleToPlayer + num438)
            {
                npc.rotation = angleToPlayer;
            }
            if (npc.rotation < 0f)
            {
                npc.rotation += 6.283f;
            }
            else if ((double)npc.rotation > 6.283)
            {
                npc.rotation -= 6.283f;
            }
            if (npc.rotation > angleToPlayer - num438 && npc.rotation < angleToPlayer + num438)
            {
                npc.rotation = angleToPlayer;
            }
            if (Main.rand.Next(5) == 0)
            {
                int dustid = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f), npc.width, (int)((float)npc.height * 0.5f), 5, npc.velocity.X, 2f);
                Main.dust[dustid].velocity.X *= 0.5f;
                Main.dust[dustid].velocity.Y *= 0.1f;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient && !Main.dayTime && !PlayerDead && npc.timeLeft < 10)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (i != npc.whoAmI && Main.npc[i].active && (Main.npc[i].type == NPCID.Retinazer || Main.npc[i].type == NPCID.Spazmatism))
                    {
                        Main.npc[i].timeLeft = 6000;
                    }
                }
            }
            if (Main.dayTime || PlayerDead)
            {
                npc.velocity.Y -= 0.04f;
                npc.timeLeft = (npc.timeLeft > 10 )? 10 : npc.timeLeft - 1 ;
                return false;
            }
            if (npc.ai[0] == 0f)
            {
                if (npc.ai[1] == 0f)
                {
                    
                    npc.TargetClosest();
                    float Speed = 20f;
                    float Acceleration = 0.9f;
                    
                    int SideOfPlayer = 1;
                    if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
                    {
                        SideOfPlayer = -1;
                    }
                    Vector2 NpcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float Xdiffer = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(SideOfPlayer * 400) - NpcCenter.X;
                    float Ydiffer = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NpcCenter.Y;
                    float Mag = (float)Math.Sqrt(Xdiffer * Xdiffer + Ydiffer * Ydiffer);
                    float Magnitude = Mag;
                    Mag = Speed / Mag;
                    Xdiffer *= Mag;
                    Ydiffer *= Mag;
                    if (npc.velocity.X < Xdiffer)
                    {
                        npc.velocity.X += Acceleration;
                        if (npc.velocity.X < 0f && Xdiffer > 0f)
                        {
                            npc.velocity.X += Acceleration;
                        }
                    }
                    else if (npc.velocity.X > Xdiffer)
                    {
                        npc.velocity.X -= Acceleration;
                        if (npc.velocity.X > 0f && Xdiffer < 0f)
                        {
                            npc.velocity.X -= Acceleration;
                        }
                    }
                    if (npc.velocity.Y < Ydiffer)
                    {
                        npc.velocity.Y += Acceleration;
                        if (npc.velocity.Y < 0f && Ydiffer > 0f)
                        {
                            npc.velocity.Y += Acceleration;
                        }
                    }
                    else if (npc.velocity.Y > Ydiffer)
                    {
                        npc.velocity.Y -= Acceleration;
                        if (npc.velocity.Y > 0f && Ydiffer < 0f)
                        {
                            npc.velocity.Y -= Acceleration;
                        }
                    }
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 600f)
                    {
                        npc.ai[1] = 1f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        npc.target = 255;
                        npc.netUpdate = true;
                    }
                    else
                    {
                        if (!Main.player[npc.target].dead)
                        {
                            npc.ai[3] += 2.5f;
                            
                        }
                        if (npc.ai[3] >= 60f)
                        {
                            npc.ai[3] = 0f;
                            NpcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                            Xdiffer = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcCenter.X;
                            Ydiffer = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NpcCenter.Y;
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                float projVel = 18f;
                                
                                int num449 = mod.ProjectileType("IchorFlame");
                                
                                Mag = (float)Math.Sqrt(Xdiffer * Xdiffer + Ydiffer * Ydiffer);
                                Mag = projVel / Mag;
                                Xdiffer *= Mag;
                                Ydiffer *= Mag;
                                Xdiffer += (float)Main.rand.Next(-40, 41) * 0.05f;
                                Ydiffer += (float)Main.rand.Next(-40, 41) * 0.05f;
                                NpcCenter.X += Xdiffer * 4f;
                                NpcCenter.Y += Ydiffer * 4f;
                                int projid = Projectile.NewProjectile(NpcCenter.X, NpcCenter.Y, Xdiffer, Ydiffer, num449, IchorShotDamage, 0f, Main.myPlayer);
                                NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                            }
                        }
                    }
                }
                else if (npc.ai[1] == 1f)
                {
                    npc.rotation = angleToPlayer;
                    float Speed = 13f;
                    
                        if ((double)npc.life < (double)npc.lifeMax * 0.9)
                        {
                            Speed += 0.5f;
                        }
                        if ((double)npc.life < (double)npc.lifeMax * 0.8)
                        {
                            Speed += 0.5f;
                        }
                        if ((double)npc.life < (double)npc.lifeMax * 0.7)
                        {
                            Speed += 0.55f;
                        }
                        if ((double)npc.life < (double)npc.lifeMax * 0.6)
                        {
                            Speed += 0.6f;
                        }
                        if ((double)npc.life < (double)npc.lifeMax * 0.5)
                        {
                            Speed += 0.65f;
                        }
                    
                    
                        Speed *= 1.5f;
                    
                    Vector2 NpcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float XDiff = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcCenter.X;
                    float YDiff = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NpcCenter.Y;
                    float Mag = (float)Math.Sqrt(XDiff * XDiff + YDiff * YDiff);
                    Mag = Speed / Mag;
                    npc.velocity.X = XDiff * Mag;
                    npc.velocity.Y = YDiff * Mag;
                    npc.ai[1] = 2f;
                }
                else if (npc.ai[1] == 2f)
                {
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 8f)
                    {
                        npc.velocity.X *= 0.98f;
                        npc.velocity.Y *= 0.98f;
                        if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                        {
                            npc.velocity.X = 0f;
                        }
                        if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                        {
                            npc.velocity.Y = 0f;
                        }
                    }
                    else
                    {
                        npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) - 1.57f;
                    }
                    if (npc.ai[2] >= 42f)
                    {
                        npc.ai[3] += 1f;
                        npc.ai[2] = 0f;
                        npc.target = 255;
                        npc.rotation = angleToPlayer;
                        if (npc.ai[3] >= 10f)
                        {
                            npc.ai[1] = 0f;
                            npc.ai[3] = 0f;
                        }
                        else
                        {
                            npc.ai[1] = 1f;
                        }
                    }
                }
                if ((double)npc.life < (double)npc.lifeMax)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                    
                    
                }
                return false;
            }
            if (npc.ai[0] == 1f || npc.ai[0] == 2f)
            {
                if (npc.ai[0] == 1f)
                {
                    npc.ai[2] += 0.005f;
                    if ((double)npc.ai[2] > 0.5)
                    {
                        npc.ai[2] = 0.5f;
                    }
                }
                else
                {
                    npc.ai[2] -= 0.005f;
                    if (npc.ai[2] < 0f)
                    {
                        npc.ai[2] = 0f;
                    }
                }
                npc.rotation += npc.ai[2];
                npc.ai[1] += 1f;
                if (npc.ai[1] == 100f)
                {
                    npc.ai[0] += 1f;
                    npc.ai[1] = 0f;
                    if (npc.ai[0] == 3f)
                    {
                        npc.ai[2] = 0f;
                    }
                    else
                    {
                        Main.PlaySound(SoundID.NPCHit, (int)npc.position.X, (int)npc.position.Y);
                        for (int i = 0; i < 2; i++)
                        {
                            Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 144);
                            Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7);
                            Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6);
                        }
                        for (int i = 0; i < 20; i++)
                        {
                            Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
                        }
                        Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
                    }
                }
                Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
                npc.velocity.X *= 0.98f;
                npc.velocity.Y *= 0.98f;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                {
                    npc.velocity.Y = 0f;
                }
                return false;
            }
            npc.HitSound = SoundID.NPCHit4;
            npc.damage = (int)((double)npc.defDamage * 1.5);
            npc.defense = npc.defDefense + 18;
            if (npc.ai[1] == 0f)
            {
                float speed = 6f;
                float acceleration = 0.2f;
                int num459 = 1;
                if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
                {
                    num459 = -1;
                }
                Vector2 NpcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float Xdiffer = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(num459 * 180) - NpcCenter.X;
                float Ydiffer = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NpcCenter.Y;
                float Mag = (float)Math.Sqrt(Xdiffer * Xdiffer + Ydiffer * Ydiffer);
                if (Main.expertMode)
                {
                    if (Mag > 300f)
                    {
                        speed += 0.5f;
                    }
                    if (Mag > 400f)
                    {
                        speed += 0.5f;
                    }
                    if (Mag > 500f)
                    {
                        speed += 0.55f;
                    }
                    if (Mag > 600f)
                    {
                        speed += 0.55f;
                    }
                    if (Mag > 700f)
                    {
                        speed += 0.6f;
                    }
                    if (Mag > 800f)
                    {
                        speed += 0.6f;
                    }
                }
                if (DRGNModWorld.MentalMode)
                {
                    speed *= 1.3f;
                    acceleration *= 1.3f;
                }
                Mag = speed / Mag;
                Xdiffer *= Mag;
                Ydiffer *= Mag;
                if (npc.velocity.X < Xdiffer)
                {
                    npc.velocity.X += acceleration;
                    if (npc.velocity.X < 0f && Xdiffer > 0f)
                    {
                        npc.velocity.X += acceleration;
                    }
                }
                else if (npc.velocity.X > Xdiffer)
                {
                    npc.velocity.X -= acceleration;
                    if (npc.velocity.X > 0f && Xdiffer < 0f)
                    {
                        npc.velocity.X -= acceleration;
                    }
                }
                if (npc.velocity.Y < Ydiffer)
                {
                    npc.velocity.Y += acceleration;
                    if (npc.velocity.Y < 0f && Ydiffer > 0f)
                    {
                        npc.velocity.Y += acceleration;
                    }
                }
                else if (npc.velocity.Y > Ydiffer)
                {
                    npc.velocity.Y -= acceleration;
                    if (npc.velocity.Y > 0f && Ydiffer < 0f)
                    {
                        npc.velocity.Y -= acceleration;
                    }
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= 400f)
                {
                    npc.ai[1] = 1f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.target = 255;
                    npc.netUpdate = true;
                }
                if (!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    return false;
                }
                npc.localAI[2] += 1f;
                if (npc.localAI[2] > 22f)
                {
                    npc.localAI[2] = 0f;
                    Main.PlaySound(SoundID.Item34, npc.position);
                }
                if (Main.netMode != 1)
                {
                    npc.localAI[1] += 1f;
                    if ((double)npc.life < (double)npc.lifeMax * 0.75)
                    {
                        npc.localAI[1] += 1f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.5)
                    {
                        npc.localAI[1] += 1f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.25)
                    {
                        npc.localAI[1] += 1f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.1)
                    {
                        npc.localAI[1] += 2f;
                    }
                    if (npc.localAI[1] > 8f)
                    {
                        npc.localAI[1] = 0f;
                        float Speed2 = 8f;
                        

                        int[] ProjTypes = new int[3] { ProjectileID.IchorArrow, ProjectileID.IchorSplash, mod.ProjectileType("IchorFlame") };
                        NpcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        Xdiffer = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcCenter.X;
                        Ydiffer = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NpcCenter.Y;
                        Mag = (float)Math.Sqrt(Xdiffer * Xdiffer + Ydiffer * Ydiffer);
                        Mag = Speed2 / Mag;
                        Xdiffer *= Mag;
                        Ydiffer *= Mag;
                        Ydiffer += (float)Main.rand.Next(-40, 41) * 0.01f;
                        Xdiffer += (float)Main.rand.Next(-40, 41) * 0.01f;
                        Ydiffer += npc.velocity.Y * 0.5f;
                        Xdiffer += npc.velocity.X * 0.5f;
                        NpcCenter.X -= Xdiffer * 1f;
                        NpcCenter.Y -= Ydiffer * 1f;
                        int projid = Projectile.NewProjectile(NpcCenter.X, NpcCenter.Y, Xdiffer, Ydiffer, Main.rand.Next(ProjTypes), IchorShotDamage, 0f, Main.myPlayer);
                        Main.projectile[projid].hostile = true;
                        Main.projectile[projid].friendly = false;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, projid);
                        }

                    }
                }
            }
                else if (npc.ai[1] == 1f)
                {
                    Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
                    npc.rotation = angleToPlayer;
                    float Speed = 20f;

                    Vector2 NpcCenter2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float Xdiffr = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcCenter2.X;
                    float Ydiffr = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NpcCenter2.Y;
                    float Mag2 = (float)Math.Sqrt(Xdiffr * Xdiffr + Ydiffr * Ydiffr);
                    Mag2 = Speed / Mag2;
                    npc.velocity.X = Xdiffr * Mag2;
                    npc.velocity.Y = Ydiffr * Mag2;
                    npc.ai[1] = 2f;
                }
            
            else
            {
                if (npc.ai[1] != 2f)
                {
                    return false;
                }
                npc.ai[2] += 1f;
                if (Main.expertMode)
                {
                    npc.ai[2] += 0.5f;
                }
                if (npc.ai[2] >= 50f)
                {
                    npc.velocity.X *= 0.93f;
                    npc.velocity.Y *= 0.93f;
                    if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                    {
                        npc.velocity.X = 0f;
                    }
                    if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
                    {
                        npc.velocity.Y = 0f;
                    }
                }
                else
                {
                    npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) - 1.57f;
                }
                if (npc.ai[2] >= 80f)
                {
                    npc.ai[3] += 1f;
                    npc.ai[2] = 0f;
                    npc.target = 255;
                    npc.rotation = angleToPlayer;
                    if (npc.ai[3] >= 6f)
                    {
                        npc.ai[1] = 0f;
                        npc.ai[3] = 0f;
                    }
                    else
                    {
                        npc.ai[1] = 1f;
                    }
                }
            }
            return false;

        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 1.0;
            if (npc.ai[0] > 1f)
            {

                if (npc.frameCounter < 7.0)
                {
                    npc.frame.Y = frameHeight * 3;
                }
                else if (npc.frameCounter < 14.0)
                {
                    npc.frame.Y = frameHeight * 4;
                }
                else if (npc.frameCounter < 21.0)
                {
                    npc.frame.Y = frameHeight * 5;
                }
                else if (npc.frameCounter < 28.0)
                {
                    npc.frame.Y = frameHeight * 6;
                }
                else 
                {
                    npc.frameCounter = 0.0;
                    npc.frame.Y = frameHeight * 3;
                }
            }
            else
            {
                if (npc.frameCounter < 7.0)
                {
                    npc.frame.Y = 0;
                }
                else if (npc.frameCounter < 14.0)
                {
                    npc.frame.Y = frameHeight;
                }
                else if (npc.frameCounter < 21.0)
                {
                    npc.frame.Y = frameHeight * 2;
                }
                else
                {
                    npc.frameCounter = 0.0;
                    npc.frame.Y = 0;
                }
            }
            
           
        }
        public override void BossHeadSlot(ref int index)
        {
            if (npc.ai[0] > 1f)
            {
                
                index = ModContent.GetModBossHeadSlot("DRGN/NPCs/Boss/Triplet_Head_Boss_1");
            }
            
        }
        public override void NPCLoot()
        {
            if (DRGNModWorld.MentalMode)
            {
                npc.DropBossBags();
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2106, 1, noBroadcast: false, -1);
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 549, Main.rand.Next(25, 41));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1225, Main.rand.Next(15, 31));
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {


            for (int i = 4; i >= 0; i --)
            {
                Vector2 oldV = npc.velocity * i;
                Vector2 vect = new Vector2(npc.position.X + npc.width/2 - Main.screenPosition.X - oldV.X , npc.position.Y + npc.height/2- Main.screenPosition.Y - oldV.Y);
                Rectangle rect = npc.frame;
               
                Color alpha9 = npc.GetAlpha(Color.White);
                alpha9.R = (byte)(alpha9.R * (10 - (2 * i)) / 20);
                alpha9.G = (byte)(alpha9.G * (10 - (2 * i)) / 20);
                alpha9.B = (byte)(alpha9.B * (10 - (2 * i)) / 20);
                alpha9.A = (byte)(alpha9.A * (10 - (2 * i)) / 20);
                spriteBatch.Draw(
                    mod.GetTexture("NPCs/Boss/Triplet"),
                     vect, rect, alpha9, npc.rotation, new Vector2(npc.width / 2, npc.height / 2), 1f, SpriteEffects.None, 0f);


                
                //SpriteBatch.Draw(mod.GetTexture("NPCs/Boss/Triplet"),
                //new Vector2(npc.oldPos[i].X - Main.screenPosition.X + (float)(npc.width / 2) - (float)110 / 2f + npc.Center.X , npc.oldPos[i].Y - Main.screenPosition.Y + (float)npc.height - (float)162 / (float)Main.npcFrameCount[npc.type] + 4f + npc.Center.Y + 30f),
                //npc.frame, alpha9, npc.rotation, npc.Center, 1f, SpriteEffects.None, 0f);
            }
            Vector2 vect2 = new Vector2(npc.position.X + npc.width / 2 - Main.screenPosition.X, npc.position.Y + npc.height / 2 - Main.screenPosition.Y);
            Rectangle rect2 = npc.frame;
            spriteBatch.Draw(
                    mod.GetTexture("NPCs/Boss/Triplet"),
                     vect2, rect2, Color.White, npc.rotation, new Vector2(npc.width / 2, npc.height / 2), 1f, SpriteEffects.None, 0f);
            return false;
                
        }
       
    }
}