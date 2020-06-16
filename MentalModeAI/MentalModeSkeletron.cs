using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.MentalModeAI
{
    public class MentalModeSkeletron : GlobalNPC


    {

        public static int shootCD;

        public override bool PreAI(NPC npc)
        {
            if (npc.type == NPCID.SkeletronHead && DRGNModWorld.MentalMode)
            {
                int DefenseInc = 0;
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].active && Main.npc[i].type == NPCID.SkeletronHand)
                    {
                        DefenseInc++;
                    }
                }
                npc.dontTakeDamage = (DefenseInc == 0 )? false : true;

                if (npc.ai[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.TargetClosest(true);
                    npc.ai[0] = 1f;

                    int Hand = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, 36, npc.whoAmI);
                    Main.npc[Hand].ai[0] = -1f;
                    Main.npc[Hand].ai[1] = (float)npc.whoAmI;
                    Main.npc[Hand].target = npc.target;
                    Main.npc[Hand].netUpdate = true;
                    Hand = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, 36, npc.whoAmI);
                    Main.npc[Hand].ai[0] = 1f;
                    Main.npc[Hand].ai[1] = (float)npc.whoAmI;
                    Main.npc[Hand].ai[3] = 150f;
                    Main.npc[Hand].target = npc.target;
                    Main.npc[Hand].netUpdate = true;
                    Hand = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, 36, npc.whoAmI);
                    Main.npc[Hand].ai[0] = -2f;
                    Main.npc[Hand].ai[1] = (float)npc.whoAmI;

                    Main.npc[Hand].target = npc.target;
                    Main.npc[Hand].netUpdate = true;
                    Hand = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, 36, npc.whoAmI);
                    Main.npc[Hand].ai[0] = 2f;
                    Main.npc[Hand].ai[1] = (float)npc.whoAmI;
                    Main.npc[Hand].ai[3] = 150f;
                    Main.npc[Hand].target = npc.target;
                    Main.npc[Hand].netUpdate = true;

                }

                if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 2000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000f)
                {
                    npc.TargetClosest(true);
                    if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 2000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000f)
                    {
                        npc.ai[1] = 3f;
                    }
                }
                if (Main.dayTime && npc.ai[1] != 3f && npc.ai[1] != 2f)
                {
                    npc.ai[1] = 2f;
                    Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
                }
                


                
                if ((DefenseInc < 2 || (double)npc.life < (double)npc.lifeMax * 0.75) && npc.ai[1] == 0f)
                {
                    float shootDelay = 40f;
                    if (DefenseInc == 0)
                    {
                        shootDelay /= 2f;
                    }
                    
                    if (Main.netMode != NetmodeID.MultiplayerClient && npc.ai[2] % shootDelay == 0f)
                    {
                        Vector2 NPCCenter = npc.Center;
                        float num160 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NPCCenter.X;
                        float num161 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NPCCenter.Y;
                        float num162 = (float)Math.Sqrt(num160 * num160 + num161 * num161);
                        if (Collision.CanHit(NPCCenter, 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                        {
                            float Speed = 4f;
                            if (DefenseInc == 0)
                            {
                                Speed += 6f;
                            }
                            float Xdiff = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - NPCCenter.X + (float)Main.rand.Next(-20, 21);
                            float Ydiff = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - NPCCenter.Y + (float)Main.rand.Next(-20, 21);
                            float Mag = (float)Math.Sqrt(Xdiff * Xdiff + Ydiff * Ydiff);
                            Mag = Speed / Mag;
                            Xdiff *= Mag;
                            Ydiff *= Mag;
                            Vector2 ShootVel = new Vector2(Xdiff * 1f + (float)Main.rand.Next(-50, 51) * 0.01f, Ydiff * 1f + (float)Main.rand.Next(-50, 51) * 0.01f);
                            ShootVel.Normalize();
                            ShootVel *= Speed;
                            ShootVel += npc.velocity;
                            Xdiff = ShootVel.X;
                            Ydiff = ShootVel.Y;
                            int dmg = npc.damage / 3;
                            int ProjType = 270;
                            NPCCenter += ShootVel * 5f;
                            int ProjID = Projectile.NewProjectile(NPCCenter.X, NPCCenter.Y, Xdiff, Ydiff, ProjType, dmg, 0f, Main.myPlayer, -1f);
                            Main.projectile[ProjID].timeLeft = 300;
                        }
                    }
                }

                if (npc.ai[1] == 0f)
                {
                    
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= 800f)
                    {
                        npc.ai[2] = 0f;
                        npc.ai[1] = 1f;
                        npc.TargetClosest(true);
                        npc.netUpdate = true;
                    }
                    npc.rotation = npc.velocity.X / 15f;
                    float maxVelY = 4f;
                    float maxVelX = 16f;
                    float AccY = 0.05f;
                    float AccX = 0.1f;
                    if (npc.position.Y > Main.player[npc.target].position.Y - 250f)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y * 0.98f;
                        }
                        npc.velocity.Y = npc.velocity.Y - AccY;
                        if (npc.velocity.Y > maxVelY)
                        {
                            npc.velocity.Y = maxVelY;
                        }
                    }
                    else
                    {
                        if (npc.position.Y < Main.player[npc.target].position.Y - 250f)
                        {
                            if (npc.velocity.Y < 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.98f;
                            }
                            npc.velocity.Y = npc.velocity.Y + AccY;
                            if (npc.velocity.Y < -maxVelY)
                            {
                                npc.velocity.Y = -maxVelY;
                            }
                        }
                    }
                    if (npc.position.X + (float)(npc.width / 2) > Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.98f;
                        }
                        npc.velocity.X = npc.velocity.X - AccX;
                        if (npc.velocity.X > maxVelX)
                        {
                            npc.velocity.X = maxVelX;
                        }
                    }
                    if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.98f;
                        }
                        npc.velocity.X = npc.velocity.X + AccX;
                        if (npc.velocity.X < -maxVelX)
                        {
                            npc.velocity.X = -maxVelX;
                        }
                    }
                }
                else
                {
                    if (npc.ai[1] == 1f)
                    {
                        npc.defense = 0;
                        npc.ai[2] += 1f;
                        if (npc.ai[2] == 2f)
                        {
                            Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
                        }
                        if (npc.ai[2] >= 400f)
                        {
                            npc.ai[2] = 0f;
                            npc.ai[1] = 0f;
                        }
                        npc.rotation += (float)npc.direction * 0.3f;
                        Vector2 NPCCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        float Xdiff = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NPCCenter.X;
                        float Ydiff = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NPCCenter.Y;
                        float Mag = (float)Math.Sqrt((double)(Xdiff * Xdiff + Ydiff * Ydiff));
                        Mag = (DefenseInc == 0)? 6f / Mag : 3f / Mag;
                        npc.velocity.X = Xdiff * Mag;
                        npc.velocity.Y = Ydiff * Mag;
                        if (shootCD <= 0)
                        {
                            Vector2 moveTo = Main.player[npc.target].Center + new Vector2(Main.rand.Next(-2, 2), Main.rand.Next(-25, 25));
                            Vector2 moveVel = (moveTo - npc.Center);
                            float magnitude = (float)Math.Sqrt((double)(moveVel.X * moveVel.X + moveVel.Y * moveVel.Y));

                            moveVel *= 16 / magnitude;
                            Projectile.NewProjectile(npc.Center, moveVel, ProjectileID.SkeletonBone, npc.damage / 5, 0f, Main.myPlayer);
                            shootCD = 10;
                        }
                        else { shootCD -= 1; }

                    }
                    else
                    {
                        if (npc.ai[1] == 2f)
                        {
                            npc.damage = 9999;
                            npc.defense = 9999;
                            npc.rotation += (float)npc.direction * 0.3f;
                            Vector2 NPCCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                            float Xdiff = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NPCCenter.X;
                            float Ydiff = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NPCCenter.Y;
                            float Mag = (float)Math.Sqrt((double)(Xdiff * Xdiff + Ydiff * Ydiff));
                            Mag = 50f / Mag;
                            npc.velocity.X = Xdiff * Mag;
                            npc.velocity.Y = Ydiff * Mag;
                        }
                        else
                        {
                            if (npc.ai[1] == 3f)
                            {
                                npc.velocity.Y = npc.velocity.Y + 0.1f;
                                if (npc.velocity.Y < 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y * 0.95f;
                                }
                                npc.velocity.X = npc.velocity.X * 0.95f;
                                if (npc.timeLeft > 500)
                                {
                                    npc.timeLeft = 500;
                                }
                            }
                        }
                    }
                }
                if (npc.ai[1] != 2f && npc.ai[1] != 3f)
                {
                    int DustID = Dust.NewDust(new Vector2(npc.position.X + (float)(npc.width / 2) - 15f - npc.velocity.X * 5f, npc.position.Y + (float)npc.height - 2f), 30, 10, 5, -npc.velocity.X * 0.2f, 3f, 0, default(Color), 2f);
                    Main.dust[DustID].noGravity = true;
                    Dust dust = Main.dust[DustID];
                    dust.velocity.X = dust.velocity.X * 1.3f;
                    Dust dust2 = Main.dust[DustID];
                    dust2.velocity.X = dust2.velocity.X + npc.velocity.X * 0.4f;
                    Dust dust3 = Main.dust[DustID];
                    dust3.velocity.Y = dust3.velocity.Y + (2f + npc.velocity.Y);
                    for (int i = 0; i < 2; i++)
                    {
                        DustID = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 120f), npc.width, 60, 5, npc.velocity.X, npc.velocity.Y, 0, default(Color), 2f);
                        Main.dust[DustID].noGravity = true;
                        Main.dust[DustID].velocity -= npc.velocity;
                        Dust dust4 = Main.dust[DustID];
                        dust4.velocity.Y = dust4.velocity.Y + 5f;
                    }
                    return false;
                }
            }














            //// Hands
            if (npc.aiStyle == 12 && DRGNModWorld.MentalMode)
            {
                if (npc.ai[0] < 2 && npc.ai[0] > -2)
                {
                    npc.spriteDirection = -(int)npc.ai[0];
                }
                else
                { npc.spriteDirection = -((int)npc.ai[0] / 2); }
                if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].aiStyle != 11)
                {
                    npc.ai[2] += 10f;
                    if (npc.ai[2] > 50f || Main.netMode != NetmodeID.Server)
                    {
                        npc.life = -1;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                    }
                }
                if (npc.ai[2] == 0f || npc.ai[2] == 3f)
                {
                    if (Main.npc[(int)npc.ai[1]].ai[1] == 3f && npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    if (Main.npc[(int)npc.ai[1]].ai[1] != 0f)
                    {
                        if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y - 100f)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.96f;
                            }
                            npc.velocity.Y = npc.velocity.Y - 0.07f;
                            if (npc.velocity.Y > 6f)
                            {
                                npc.velocity.Y = 6f;
                            }
                        }
                        else
                        {
                            if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 100f)
                            {
                                if (npc.velocity.Y < 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y * 0.96f;
                                }
                                npc.velocity.Y = npc.velocity.Y + 0.07f;
                                if (npc.velocity.Y < -6f)
                                {
                                    npc.velocity.Y = -6f;
                                }
                            }
                        }
                        if (npc.position.X + (float)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 120f * npc.ai[0])
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.96f;
                            }
                            npc.velocity.X = npc.velocity.X - 0.1f;
                            if (npc.velocity.X > 8f)
                            {
                                npc.velocity.X = 8f;
                            }
                        }
                        if (npc.position.X + (float)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 120f * npc.ai[0])
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.96f;
                            }
                            npc.velocity.X = npc.velocity.X + 0.1f;
                            if (npc.velocity.X < -8f)
                            {
                                npc.velocity.X = -8f;
                            }
                        }
                    }
                    else
                    {
                        npc.ai[3] += 1f;
                        if (npc.ai[3] >= 300f)
                        {
                            npc.ai[2] += 1f;
                            npc.ai[3] = 0f;
                            npc.netUpdate = true;
                        }
                        if (npc.ai[0] < 2 && npc.ai[0] > -2)
                        {
                            if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y + 230f)
                            {
                                if (npc.velocity.Y > 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y * 0.96f;
                                }
                                npc.velocity.Y = npc.velocity.Y - 0.04f;
                                if (npc.velocity.Y > 3f)
                                {
                                    npc.velocity.Y = 3f;
                                }
                            }
                            else
                            {
                                if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y + 230f)
                                {
                                    if (npc.velocity.Y < 0f)
                                    {
                                        npc.velocity.Y = npc.velocity.Y * 0.96f;
                                    }
                                    npc.velocity.Y = npc.velocity.Y + 0.04f;
                                    if (npc.velocity.Y < -3f)
                                    {
                                        npc.velocity.Y = -3f;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y - 230f)
                            {
                                if (npc.velocity.Y > 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y * 0.96f;
                                }
                                npc.velocity.Y = npc.velocity.Y - 0.04f;
                                if (npc.velocity.Y > 3f)
                                {
                                    npc.velocity.Y = 3f;
                                }
                            }
                            else
                            {
                                if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 230f)
                                {
                                    if (npc.velocity.Y < 0f)
                                    {
                                        npc.velocity.Y = npc.velocity.Y * 0.96f;
                                    }
                                    npc.velocity.Y = npc.velocity.Y + 0.04f;
                                    if (npc.velocity.Y < -3f)
                                    {
                                        npc.velocity.Y = -3f;
                                    }
                                }
                            }
                        }
                        if (npc.position.X + (float)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0])
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.96f;
                            }
                            npc.velocity.X = npc.velocity.X - 0.07f;
                            if (npc.velocity.X > 8f)
                            {
                                npc.velocity.X = 8f;
                            }
                        }
                        if (npc.position.X + (float)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0])
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.96f;
                            }
                            npc.velocity.X = npc.velocity.X + 0.07f;
                            if (npc.velocity.X < -8f)
                            {
                                npc.velocity.X = -8f;
                            }
                        }
                    }
                    Vector2 NpcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float HeadXdiff = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0] - NpcCenter.X;
                    float HeadYdiff = Main.npc[(int)npc.ai[1]].position.Y - 230f - NpcCenter.Y;
                    if (npc.ai[0] < 2 && npc.ai[0] > -2)
                    {
                        HeadYdiff = Main.npc[(int)npc.ai[1]].position.Y + 230f - NpcCenter.Y;
                    }


                    Math.Sqrt((double)(HeadXdiff * HeadXdiff + HeadYdiff * HeadYdiff));
                    npc.rotation = (float)Math.Atan2((double)HeadYdiff, (double)HeadXdiff) + 1.57f;
                    return false;
                }
                if (npc.ai[2] == 1f)
                {
                    Vector2 HandPos = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float Xdiff = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0] - HandPos.X;
                    float Ydiff = Main.npc[(int)npc.ai[1]].position.Y + 230f - HandPos.Y;
                    float Mag = (float)Math.Sqrt((double)(Xdiff * Xdiff + Ydiff * Ydiff));
                    npc.rotation = (float)Math.Atan2((double)Ydiff, (double)Xdiff) + 1.57f;
                    npc.velocity.X = npc.velocity.X * 0.95f;
                    npc.velocity.Y = npc.velocity.Y - 0.1f;
                    if (npc.velocity.Y < -8f)
                    {
                        npc.velocity.Y = -8f;
                    }
                    if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 200f)
                    {
                        npc.TargetClosest(true);
                        npc.ai[2] = 2f;
                        HandPos = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        Xdiff = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - HandPos.X;
                        Ydiff = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - HandPos.Y;
                        Mag = (float)Math.Sqrt((double)(Xdiff * Xdiff + Ydiff * Ydiff));
                        Mag = 25f / Mag;
                        npc.velocity.X = Xdiff * Mag;
                        npc.velocity.Y = Ydiff * Mag;
                        npc.netUpdate = true;
                        return false;
                    }
                }
                else
                {
                    if (npc.ai[2] == 2f)
                    {
                        if (npc.position.Y > Main.player[npc.target].position.Y || npc.velocity.Y < 0f)
                        {
                            npc.ai[2] = 3f;
                            return false;
                        }
                    }
                    else
                    {
                        if (npc.ai[2] == 4f)
                        {
                            Vector2 NpcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                            float HeadXdiff = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0] - NpcCenter.X;
                            float HeadYdiff = Main.npc[(int)npc.ai[1]].position.Y + 230f - NpcCenter.Y;
                            float Mag = (float)Math.Sqrt((double)(HeadXdiff * HeadXdiff + HeadYdiff * HeadYdiff));
                            npc.rotation = (float)Math.Atan2((double)HeadYdiff, (double)HeadXdiff) + 1.57f;
                            npc.velocity.Y = npc.velocity.Y * 0.95f;
                            npc.velocity.X = npc.velocity.X + 0.1f * -npc.ai[0];
                            if (npc.velocity.X < -15f)
                            {
                                npc.velocity.X = -15f;
                            }
                            if (npc.velocity.X > 15f)
                            {
                                npc.velocity.X = 15f;
                            }
                            if (npc.position.X + (float)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - 500f || npc.position.X + (float)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) + 500f)
                            {
                                npc.TargetClosest(true);
                                npc.ai[2] = 5f;
                                NpcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                                HeadXdiff = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - NpcCenter.X;
                                HeadYdiff = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - NpcCenter.Y;
                                Mag = (float)Math.Sqrt((double)(HeadXdiff * HeadXdiff + HeadYdiff * HeadYdiff));
                                Mag = 25f / Mag;
                                npc.velocity.X = HeadXdiff * Mag;
                                npc.velocity.Y = HeadYdiff * Mag;
                                npc.netUpdate = true;
                                return false;
                            }
                        }
                        else
                        {
                            if (npc.ai[2] == 5f && ((npc.velocity.X > 0f && npc.position.X + (float)(npc.width / 2) > Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2)) || (npc.velocity.X < 0f && npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))))
                            {
                                npc.ai[2] = 0f;
                                return false;
                            }
                        }

                    }



                }

            }


            if ((npc.type == NPCID.SkeletronHead || npc.aiStyle == 12) && DRGNModWorld.MentalMode)
            { return false; }
            else { return true; }
        }
    }
}