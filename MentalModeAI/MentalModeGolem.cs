using DRGN.Projectiles;
using Microsoft.Xna.Framework;
using Steamworks;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.MentalModeAI
{
    public class MentalModeGolem : GlobalNPC


    {


        
        public override bool PreAI(NPC npc)
        {
            if (DRGNModWorld.MentalMode)
            {
				if (npc.aiStyle == 45)
				{
					NPC.golemBoss = npc.whoAmI;
					float num673 = 3 * Main.ActivePlayersCount;
					if (DRGNModWorld.MentalMode)
					{
						num673 += 2f;
					}
					if ((!Main.player[npc.target].ZoneRockLayerHeight && !Main.player[npc.target].ZoneJungle) || (double)Main.player[npc.target].Center.Y < Main.worldSurface * 16.0)
					{
						num673 *= 2f;
					}
					if (npc.localAI[0] == 0f)
					{
						npc.localAI[0] = 1f;
						NPC.NewNPC((int)npc.Center.X - 84, (int)npc.Center.Y - 9, 247);
						NPC.NewNPC((int)npc.Center.X + 78, (int)npc.Center.Y - 9, 248);
						NPC.NewNPC((int)npc.Center.X - 3, (int)npc.Center.Y - 57, 246);
					}
					if (npc.target >= 0 && Main.player[npc.target].dead)
					{
						npc.TargetClosest();
						if (Main.player[npc.target].dead)
						{
							npc.noTileCollide = true;
						}
					}
					if (npc.alpha > 0)
					{
						npc.alpha -= 10;
						if (npc.alpha < 0)
						{
							npc.alpha = 0;
						}
						npc.ai[1] = 0f;
					}
					bool HeadNoFloat = false;
					bool LeftFist = false;
					bool RightFist = false;
					npc.dontTakeDamage = false;
					for (int i = 0; i < 200; i++)
					{
						if (Main.npc[i].active && Main.npc[i].type == NPCID.GolemHead)
						{
							HeadNoFloat = true;
						}
						if (Main.npc[i].active && Main.npc[i].type == NPCID.GolemFistLeft)
						{
							LeftFist = true;
						}
						if (Main.npc[i].active && Main.npc[i].type == NPCID.GolemFistRight)
						{
							RightFist = true;
						}
					}
					npc.dontTakeDamage = HeadNoFloat;
					npc.position += Vector2.Zero;
					if (!LeftFist)
					{
						int num675 = Dust.NewDust(new Vector2(npc.Center.X - 80f * npc.scale, npc.Center.Y - 9f), 8, 8, 31, 0f, 0f, 100);
						Dust dust = Main.dust[num675];
						dust.alpha += Main.rand.Next(100);
						dust = Main.dust[num675];
						dust.velocity *= 0.2f;
						Main.dust[num675].velocity.Y -= 0.5f + (float)Main.rand.Next(10) * 0.1f;
						Main.dust[num675].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
						if (Main.rand.Next(10) == 0)
						{
							num675 = Dust.NewDust(new Vector2(npc.Center.X - 80f * npc.scale, npc.Center.Y - 9f), 8, 8, 6);
							if (Main.rand.Next(20) != 0)
							{
								Main.dust[num675].noGravity = true;
								dust = Main.dust[num675];
								dust.scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
								Main.dust[num675].velocity.Y -= 1f;
							}
						}
					}
					if (!RightFist)
					{
						int num676 = Dust.NewDust(new Vector2(npc.Center.X + 62f * npc.scale, npc.Center.Y - 9f), 8, 8, 31, 0f, 0f, 100);
						Dust dust = Main.dust[num676];
						dust.alpha += Main.rand.Next(100);
						dust = Main.dust[num676];
						dust.velocity *= 0.2f;
						Main.dust[num676].velocity.Y -= 0.5f + (float)Main.rand.Next(10) * 0.1f;
						Main.dust[num676].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
						if (Main.rand.Next(10) == 0)
						{
							num676 = Dust.NewDust(new Vector2(npc.Center.X + 62f * npc.scale, npc.Center.Y - 9f), 8, 8, 6);
							if (Main.rand.Next(20) != 0)
							{
								Main.dust[num676].noGravity = true;
								dust = Main.dust[num676];
								dust.scale *= 1f + (float)Main.rand.Next(10) * 0.1f;
								Main.dust[num676].velocity.Y -= 1f;
							}
						}
					}
					npc.position -= Vector2.Zero;
					if (npc.noTileCollide && !Main.player[npc.target].dead)
					{
						if (npc.velocity.Y > 0f && npc.Bottom.Y > Main.player[npc.target].Top.Y)
						{
							npc.noTileCollide = false;
						}
						else if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].Center, 1, 1) && !SolidTiles(npc.position,npc.width, npc.height))
						{
							npc.noTileCollide = false;
						}
					}
					if (npc.ai[0] == 0f)
					{
						if (npc.velocity.Y == 0f)
						{
							npc.velocity.X *= 0.8f;
							float jumpCD = 1f;
							if (npc.ai[1] > 0f)
							{
								if (!LeftFist)
								{
									jumpCD += 2f;
								}
								if (!RightFist)
								{
									jumpCD += 2f;
								}
								if (!HeadNoFloat)
								{
									jumpCD += 2f;
								}
								if (npc.life < npc.lifeMax)
								{
									jumpCD += 1f;
								}
								if (npc.life < npc.lifeMax / 2)
								{
									jumpCD += 4f;
								}
								if (npc.life < npc.lifeMax / 3)
								{
									jumpCD += 8f;
								}
								jumpCD *= num673;
								if (DRGNModWorld.MentalMode)
								{
									jumpCD += 100f;
								}
							}
							npc.ai[1] += jumpCD;
							if (npc.ai[1] >= 300f)
							{
								npc.ai[1] = -20f;
								npc.frameCounter = 0.0;
							}
							else if (npc.ai[1] == -1f)
							{
								npc.noTileCollide = true;
								npc.TargetClosest();
								npc.velocity.X = 4 * npc.direction;
								if (npc.life < npc.lifeMax)
								{
									npc.velocity.Y = -12.1f * (num673 + 9f) / 10f;
									if ((double)npc.velocity.Y < -19.1)
									{
										npc.velocity.Y = -19.1f;
									}
								}
								else
								{
									npc.velocity.Y = -12.1f;
								}
								npc.ai[0] = 1f;
								npc.ai[1] = 0f;
							}
						}
					}
					else if (npc.ai[0] == 1f)
					{
						if (npc.velocity.Y == 0f)
						{
							Main.PlaySound(SoundID.Item14, npc.position);
							npc.ai[0] = 0f;
							for (int num678 = (int)npc.position.X - 20; num678 < (int)npc.position.X + npc.width + 40; num678 += 20)
							{
								for (int num679 = 0; num679 < 4; num679++)
								{
									int num680 = Dust.NewDust(new Vector2(npc.position.X - 20f, npc.position.Y + (float)npc.height), npc.width + 20, 4, 31, 0f, 0f, 100, default(Color), 1.5f);
									Dust dust = Main.dust[num680];
									dust.velocity *= 0.2f;
								}
								int num681 = Gore.NewGore(new Vector2(num678 - 20, npc.position.Y + (float)npc.height - 8f), default(Vector2), Main.rand.Next(61, 64));
								Gore gore = Main.gore[num681];
								gore.velocity *= 0.4f;
							}
						}
						else
						{
							npc.TargetClosest();
							if (npc.position.X < Main.player[npc.target].position.X && npc.position.X + (float)npc.width > Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
							{
								npc.velocity.X *= 0.9f;
								if (npc.Bottom.Y < Main.player[npc.target].position.Y)
								{
									npc.velocity.Y += 0.2f * (num673 + 1f) / 2f;
								}
							}
							else
							{
								if (npc.direction < 0)
								{
									npc.velocity.X -= 0.2f;
								}
								else if (npc.direction > 0)
								{
									npc.velocity.X += 0.2f;
								}
								float num682 = 3f;
								if (npc.life < npc.lifeMax)
								{
									num682 += 1f;
								}
								if (npc.life < npc.lifeMax / 2)
								{
									num682 += 1f;
								}
								if (npc.life < npc.lifeMax / 4)
								{
									num682 += 1f;
								}
								num682 *= (num673 + 1f) / 2f;
								if (npc.velocity.X < 0f - num682)
								{
									npc.velocity.X = 0f - num682;
								}
								if (npc.velocity.X > num682)
								{
									npc.velocity.X = num682;
								}
							}
						}
					}
					if (npc.target <= 0 || npc.target == 255 || Main.player[npc.target].dead)
					{
						npc.TargetClosest();
					}
					int num683 = 3000;
					if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num683)
					{
						npc.TargetClosest();
						if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num683)
						{
							npc.active = false;
						}
					}
				}
				else if (npc.aiStyle == 46)
				{
					float num684 = 3 * Main.ActivePlayersCount;
					if (DRGNModWorld.MentalMode)
					{
						num684 += 3f;
					}
					if ((!Main.player[npc.target].ZoneRockLayerHeight && !Main.player[npc.target].ZoneJungle) || (double)Main.player[npc.target].Center.Y < Main.worldSurface * 16.0)
					{
						num684 *= 2f;
					}
					npc.noTileCollide = true;
					if (NPC.golemBoss < 0)
					{
						npc.StrikeNPCNoInteraction(9999, 0f, 0);
						return false;
					}
					float num685 = 100f;
					Vector2 vector77 = new Vector2(npc.Center.X, npc.Center.Y);
					float num686 = Main.npc[NPC.golemBoss].Center.X - vector77.X;
					float num687 = Main.npc[NPC.golemBoss].Center.Y - vector77.Y;
					num687 -= 57f;
					num686 -= 3f;
					if (DRGNModWorld.MentalMode)
					{
						num686 += 1f;
						num687 += 20f;
					}
					float num688 = (float)Math.Sqrt(num686 * num686 + num687 * num687);
					if (num688 < num685)
					{
						npc.rotation = 0f;
						npc.velocity.X = num686;
						npc.velocity.Y = num687;
					}
					else
					{
						num688 = num685 / num688;
						npc.velocity.X = num686 * num688;
						npc.velocity.Y = num687 * num688;
						npc.rotation = npc.velocity.X * 0.1f;
					}
					if (npc.alpha > 0)
					{
						npc.alpha -= 10;
						if (npc.alpha < 0)
						{
							npc.alpha = 0;
						}
						npc.ai[1] = 30f;
					}
					if (npc.ai[0] == 0f)
					{
						npc.ai[1] += 1f;
						int num689 = 300;
						if (npc.ai[1] < 20f || npc.ai[1] > (float)(num689 - 20))
						{
							npc.ai[1] += 2f * (num684 - 1f) / 3f;
							npc.localAI[0] = 1f;
						}
						else
						{
							npc.ai[1] += 1f * (num684 - 1f) / 2f;
							npc.localAI[0] = 0f;
						}
						if (npc.ai[1] >= (float)num689)
						{
							npc.TargetClosest();
							npc.ai[1] = 0f;
							Vector2 vector78 = new Vector2(npc.Center.X, npc.Center.Y + 10f);
							float num690 = 8f;
							float num691 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector78.X;
							float num692 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector78.Y;
							float num693 = (float)Math.Sqrt(num691 * num691 + num692 * num692);
							num693 = num690 / num693;
							num691 *= num693;
							num692 *= num693;
							int num694 = 18;
							int num695 = 258;
							if (Main.netMode != 1)
							{
								int num696 = Projectile.NewProjectile(vector78.X, vector78.Y, num691, num692, num695, num694, 0f, Main.myPlayer);
							}
						}
					}
					else if (npc.ai[0] == 1f)
					{
						npc.TargetClosest();
						Vector2 vector79 = new Vector2(npc.Center.X, npc.Center.Y + 10f);
						if (Main.player[npc.target].Center.X < npc.Center.X - (float)npc.width)
						{
							npc.localAI[1] = -1f;
							vector79.X -= 40f;
						}
						else if (Main.player[npc.target].Center.X > npc.Center.X + (float)npc.width)
						{
							npc.localAI[1] = 1f;
							vector79.X += 40f;
						}
						else
						{
							npc.localAI[1] = 0f;
						}
						float num697 = (num684 + 3f) / 4f;
						npc.ai[1] += num697;
						if ((double)npc.life < (double)npc.lifeMax * 0.4)
						{
							npc.ai[1] += num697;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.2)
						{
							npc.ai[1] += num697;
						}
						int num698 = 300;
						if (npc.ai[1] < 20f || npc.ai[1] > (float)(num698 - 20))
						{
							npc.localAI[0] = 1f;
						}
						else
						{
							npc.localAI[0] = 0f;
						}
						if (npc.ai[1] >= (float)num698)
						{
							npc.TargetClosest();
							npc.ai[1] = 0f;
							float num699 = 8f;
							float num700 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector79.X;
							float num701 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector79.Y;
							float num702 = (float)Math.Sqrt(num700 * num700 + num701 * num701);
							num702 = num699 / num702;
							num700 *= num702;
							num701 *= num702;
							int num703 = 24;
							int num704 = 258;
							if (Main.netMode != 1)
							{
								int num705 = Projectile.NewProjectile(vector79.X, vector79.Y, num700, num701, num704, num703, 0f, Main.myPlayer);
							}
						}
						npc.ai[2] += num697;
						if (npc.life < npc.lifeMax / 3)
						{
							npc.ai[2] += num697;
						}
						if (npc.life < npc.lifeMax / 4)
						{
							npc.ai[2] += num697;
						}
						if (npc.life < npc.lifeMax / 5)
						{
							npc.ai[2] += num697;
						}
						if (!Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
						{
							npc.ai[2] += 4f;
						}
						if (npc.ai[2] > (float)(60 + Main.rand.Next(600)))
						{
							npc.ai[2] = 0f;
							int num706 = 28;
							int num707 = 259;
							if (npc.localAI[1] == 0f)
							{
								for (int num708 = 0; num708 < 2; num708++)
								{
									vector79 = new Vector2(npc.Center.X, npc.Center.Y - 22f);
									if (num708 == 0)
									{
										vector79.X -= 18f;
									}
									else
									{
										vector79.X += 18f;
									}
									float num709 = 11f;
									float num710 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector79.X;
									float num711 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector79.Y;
									float num712 = (float)Math.Sqrt(num710 * num710 + num711 * num711);
									num712 = num709 / num712;
									num710 *= num712;
									num711 *= num712;
									vector79.X += num710 * 3f;
									vector79.Y += num711 * 3f;
									if (Main.netMode != 1)
									{
										int num713 = Projectile.NewProjectile(vector79.X, vector79.Y, num710, num711, num707, num706, 0f, Main.myPlayer);
										Main.projectile[num713].timeLeft = 300;
									}
								}
							}
							else if (npc.localAI[1] != 0f)
							{
								vector79 = new Vector2(npc.Center.X, npc.Center.Y - 22f);
								if (npc.localAI[1] == -1f)
								{
									vector79.X -= 30f;
								}
								else if (npc.localAI[1] == 1f)
								{
									vector79.X += 30f;
								}
								float num714 = 12f;
								float num715 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector79.X;
								float num716 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector79.Y;
								float num717 = (float)Math.Sqrt(num715 * num715 + num716 * num716);
								num717 = num714 / num717;
								num715 *= num717;
								num716 *= num717;
								vector79.X += num715 * 3f;
								vector79.Y += num716 * 3f;
								if (Main.netMode != 1)
								{
									int num718 = Projectile.NewProjectile(vector79.X, vector79.Y, num715, num716, num707, num706, 0f, Main.myPlayer);
									Main.projectile[num718].timeLeft = 300;
								}
							}
						}
					}
					if (npc.life < npc.lifeMax / 2)
					{
						npc.ai[0] = 1f;
					}
					else
					{
						npc.ai[0] = 0f;
					}
				}
				else if (npc.aiStyle == 47)
				{
					float num719 = 3 * Main.ActivePlayersCount;
					if (DRGNModWorld.MentalMode)
					{
						num719 += 3f;
					}
					if ((!Main.player[npc.target].ZoneRockLayerHeight && !Main.player[npc.target].ZoneJungle) || (double)Main.player[npc.target].Center.Y < Main.worldSurface * 16.0)
					{
						num719 *= 2f;
					}
					if (NPC.golemBoss < 0)
					{
						npc.StrikeNPCNoInteraction(9999, 0f, 0);
						return false;
					}
					if (npc.alpha > 0)
					{
						npc.alpha -= 10;
						if (npc.alpha < 0)
						{
							npc.alpha = 0;
						}
						npc.ai[1] = 0f;
					}
					if (npc.ai[0] == 0f)
					{
						npc.noTileCollide = true;
						float num720 = 14f;
						if (npc.life < npc.lifeMax / 2)
						{
							num720 += 3f;
						}
						if (npc.life < npc.lifeMax / 4)
						{
							num720 += 3f;
						}
						if (Main.npc[NPC.golemBoss].life < Main.npc[NPC.golemBoss].lifeMax)
						{
							num720 += 8f;
						}
						num720 *= (num719 + 3f) / 4f;
						if (num720 > 32f)
						{
							num720 = 32f;
						}
						Vector2 vector80 = new Vector2(npc.Center.X, npc.Center.Y);
						float num721 = Main.npc[NPC.golemBoss].Center.X - vector80.X;
						float num722 = Main.npc[NPC.golemBoss].Center.Y - vector80.Y;
						num722 -= 9f;
						num721 = ((npc.type != 247) ? (num721 + 78f) : (num721 - 84f));
						if (DRGNModWorld.MentalMode)
						{
							num721 = ((npc.type != 247) ? (num721 - 40f) : (num721 + 40f));
						}
						float num723 = (float)Math.Sqrt(num721 * num721 + num722 * num722);
						if (num723 < 12f + num720)
						{
							npc.rotation = 0f;
							npc.velocity.X = num721;
							npc.velocity.Y = num722;
							float num724 = num719;
							npc.ai[1] += num724;
							if (npc.life < npc.lifeMax / 2)
							{
								npc.ai[1] += num724;
							}
							if (npc.life < npc.lifeMax / 4)
							{
								npc.ai[1] += num724;
							}
							if (Main.npc[NPC.golemBoss].life < Main.npc[NPC.golemBoss].lifeMax)
							{
								npc.ai[1] += 10f * num724;
							}
							if (npc.ai[1] >= 60f)
							{
								npc.TargetClosest();
								if ((npc.type == 247 && npc.Center.X + 100f > Main.player[npc.target].Center.X) || (npc.type == 248 && npc.Center.X - 100f < Main.player[npc.target].Center.X))
								{
									npc.ai[1] = 0f;
									npc.ai[0] = 1f;
								}
								else
								{
									npc.ai[1] = 0f;
								}
							}
						}
						else
						{
							num723 = num720 / num723;
							npc.velocity.X = num721 * num723;
							npc.velocity.Y = num722 * num723;
							npc.rotation = (float)Math.Atan2(0f - npc.velocity.Y, 0f - npc.velocity.X);
							if (npc.type == 247)
							{
								npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
							}
						}
					}
					else if (npc.ai[0] == 1f)
					{
						npc.noTileCollide = true;
						npc.collideX = false;
						npc.collideY = false;
						float num725 = 12f;
						if (npc.life < npc.lifeMax / 2)
						{
							num725 += 4f;
						}
						if (npc.life < npc.lifeMax / 4)
						{
							num725 += 4f;
						}
						if (Main.npc[NPC.golemBoss].life < Main.npc[NPC.golemBoss].lifeMax)
						{
							num725 += 10f;
						}
						num725 *= (num719 + 3f) / 4f;
						if (num725 > 48f)
						{
							num725 = 48f;
						}
						Vector2 vector81 = new Vector2(npc.Center.X, npc.Center.Y);
						float num726 = Main.player[npc.target].Center.X - vector81.X;
						float num727 = Main.player[npc.target].Center.Y - vector81.Y;
						float num728 = (float)Math.Sqrt(num726 * num726 + num727 * num727);
						num728 = num725 / num728;
						npc.velocity.X = num726 * num728;
						npc.velocity.Y = num727 * num728;
						npc.ai[0] = 2f;
						npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
						if (npc.type == 247)
						{
							npc.rotation = (float)Math.Atan2(0f - npc.velocity.Y, 0f - npc.velocity.X);
						}
					}
					else if (npc.ai[0] == 2f)
					{
						if (Math.Abs(npc.velocity.X) > Math.Abs(npc.velocity.Y))
						{
							if (npc.velocity.X > 0f && npc.Center.X > Main.player[npc.target].Center.X)
							{
								npc.noTileCollide = false;
							}
							if (npc.velocity.X < 0f && npc.Center.X < Main.player[npc.target].Center.X)
							{
								npc.noTileCollide = false;
							}
						}
						else
						{
							if (npc.velocity.Y > 0f && npc.Center.Y > Main.player[npc.target].Center.Y)
							{
								npc.noTileCollide = false;
							}
							if (npc.velocity.Y < 0f && npc.Center.Y < Main.player[npc.target].Center.Y)
							{
								npc.noTileCollide = false;
							}
						}
						Vector2 vector82 = new Vector2(npc.Center.X, npc.Center.Y);
						float num729 = Main.npc[NPC.golemBoss].Center.X - vector82.X;
						float num730 = Main.npc[NPC.golemBoss].Center.Y - vector82.Y;
						num729 += Main.npc[NPC.golemBoss].velocity.X;
						num730 += Main.npc[NPC.golemBoss].velocity.Y;
						num730 -= 9f;
						num729 = ((npc.type != 247) ? (num729 + 78f) : (num729 - 84f));
						float num731 = (float)Math.Sqrt(num729 * num729 + num730 * num730);
						if (Main.npc[NPC.golemBoss].life < Main.npc[NPC.golemBoss].lifeMax)
						{
							npc.knockBackResist = 0f;
							if (num731 > 700f || npc.collideX || npc.collideY)
							{
								npc.noTileCollide = true;
								npc.ai[0] = 0f;
							}
							return false;
						}
						bool flag41 = npc.justHit;
						if (flag41)
						{
							for (int i = 0; i < 200; i++)
							{
								if (!Main.npc[i].active || Main.npc[i].type != NPCID.GolemHead)
								{
									continue;
								}
								if (Main.npc[i].life < Main.npc[i].lifeMax / 2)
								{
									if (npc.knockBackResist == 0f)
									{
										flag41 = false;
									}
									npc.knockBackResist = 0f;
								}
								break;
							}
						}
						if (num731 > 600f || npc.collideX || npc.collideY || flag41)
						{
							npc.noTileCollide = true;
							npc.ai[0] = 0f;
						}
					}
					else
					{
						if (npc.ai[0] != 3f)
						{
							return false;
						}
						npc.noTileCollide = true;
						float num733 = 12f;
						float num734 = 0.4f;
						Vector2 vector83 = new Vector2(npc.Center.X, npc.Center.Y);
						float num735 = Main.player[npc.target].Center.X - vector83.X;
						float num736 = Main.player[npc.target].Center.Y - vector83.Y;
						float num737 = (float)Math.Sqrt(num735 * num735 + num736 * num736);
						num737 = num733 / num737;
						num735 *= num737;
						num736 *= num737;
						if (npc.velocity.X < num735)
						{
							npc.velocity.X += num734;
							if (npc.velocity.X < 0f && num735 > 0f)
							{
								npc.velocity.X += num734 * 2f;
							}
						}
						else if (npc.velocity.X > num735)
						{
							npc.velocity.X -= num734;
							if (npc.velocity.X > 0f && num735 < 0f)
							{
								npc.velocity.X -= num734 * 2f;
							}
						}
						if (npc.velocity.Y < num736)
						{
							npc.velocity.Y += num734;
							if (npc.velocity.Y < 0f && num736 > 0f)
							{
								npc.velocity.Y += num734 * 2f;
							}
						}
						else if (npc.velocity.Y > num736)
						{
							npc.velocity.Y -= num734;
							if (npc.velocity.Y > 0f && num736 < 0f)
							{
								npc.velocity.Y -= num734 * 2f;
							}
						}
						npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
						if (npc.type == 247)
						{
							npc.rotation = (float)Math.Atan2(0f - npc.velocity.Y, 0f - npc.velocity.X);
						}
					}
				}
				else if (npc.aiStyle == 48)
				{
					bool CannotHit = false;
					float Scaling = 3 * Main.ActivePlayersCount;
					if (DRGNModWorld.MentalMode)
					{
						Scaling += 3f;
					}
					if ((!Main.player[npc.target].ZoneRockLayerHeight && !Main.player[npc.target].ZoneJungle) || (double)Main.player[npc.target].Center.Y < Main.worldSurface * 16.0)
					{
						Scaling *= 2f;
					}
					if (!Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
					{
						npc.noTileCollide = true;
						CannotHit = true;
					}
					else if (npc.noTileCollide && SolidTiles(npc.position, npc.width, npc.height))
					{
						npc.noTileCollide = false;
					}
					if (NPC.golemBoss < 0)
					{
						npc.StrikeNPCNoInteraction(9999, 0f, 0);
						return false;
					}
					npc.TargetClosest();
					float num739 = 7f;
					float num740 = 0.05f;
					Vector2 vector84 = new Vector2(npc.Center.X, npc.Center.Y);
					float num741 = Main.player[npc.target].Center.X - vector84.X;
					float num742 = Main.player[npc.target].Center.Y - vector84.Y - 300f;
					float num743 = (float)Math.Sqrt(num741 * num741 + num742 * num742);
					num743 = num739 / num743;
					num741 *= num743;
					num742 *= num743;
					if (npc.velocity.X < num741)
					{
						npc.velocity.X += num740;
						if (npc.velocity.X < 0f && num741 > 0f)
						{
							npc.velocity.X += num740;
						}
					}
					else if (npc.velocity.X > num741)
					{
						npc.velocity.X -= num740;
						if (npc.velocity.X > 0f && num741 < 0f)
						{
							npc.velocity.X -= num740;
						}
					}
					if (npc.velocity.Y < num742)
					{
						npc.velocity.Y += num740;
						if (npc.velocity.Y < 0f && num742 > 0f)
						{
							npc.velocity.Y += num740;
						}
					}
					else if (npc.velocity.Y > num742)
					{
						npc.velocity.Y -= num740;
						if (npc.velocity.Y > 0f && num742 < 0f)
						{
							npc.velocity.Y -= num740;
						}
					}
					float ShootingCounter = (Scaling + 4f) / 5f;
					npc.ai[1] += ShootingCounter;
					if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax * 0.8)
					{
						npc.ai[1] += ShootingCounter;
					}
					if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax * 0.6)
					{
						npc.ai[1] += ShootingCounter;
					}
					if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax * 0.2)
					{
						npc.ai[1] += ShootingCounter;
					}
					if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax * 0.1)
					{
						npc.ai[1] += ShootingCounter;
					}
					int ShootCD = 300;
					if (npc.ai[1] < 20f || npc.ai[1] > (float)(ShootCD - 20))
					{
						npc.localAI[0] = 1f;
					}
					else
					{
						npc.localAI[0] = 0f;
					}
					if (CannotHit)
					{
						npc.ai[1] = 20f;
					}
					if (npc.ai[1] >= (float)ShootCD)
					{
						npc.TargetClosest();
						npc.ai[1] = 0f;
						Vector2 HeadPos = new Vector2(npc.Center.X, npc.Center.Y - 10f);
						float Speed = 8f;
						int Damage = 20;
						int Type = 258;
						float Xdiff = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - HeadPos.X;
						float Ydiff = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - HeadPos.Y;
						float Mag = (float)Math.Sqrt(Xdiff * Xdiff + Ydiff * Ydiff);
						Mag = Speed / Mag;
						Xdiff *= Mag;
						Ydiff *= Mag;
						if (Main.netMode != 1)
						{
							int num752 = Projectile.NewProjectile(HeadPos.X, HeadPos.Y, Xdiff, Ydiff, Type, Damage, 0f, Main.myPlayer);
							Projectile.NewProjectile(HeadPos, new Vector2(Xdiff, Ydiff), ModContent.ProjectileType<GiantSpikyBall>(), npc.damage/3, 0f);
						}
					}
					float Counter = Scaling;
					npc.ai[2] += Counter;
					if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax / 1.25)
					{
						npc.ai[2] += Counter;
					}
					if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax / 1.5)
					{
						npc.ai[2] += Counter;
					}
					if (Main.npc[NPC.golemBoss].life < Main.npc[NPC.golemBoss].lifeMax / 2)
					{
						npc.ai[2] += Counter;
					}
					if (Main.npc[NPC.golemBoss].life < Main.npc[NPC.golemBoss].lifeMax / 3)
					{
						npc.ai[2] += Counter;
					}
					if (Main.npc[NPC.golemBoss].life < Main.npc[NPC.golemBoss].lifeMax / 4)
					{
						npc.ai[2] += Counter;
					}
					if (Main.npc[NPC.golemBoss].life < Main.npc[NPC.golemBoss].lifeMax / 5)
					{
						npc.ai[2] += Counter;
					}
					if (Main.npc[NPC.golemBoss].life < Main.npc[NPC.golemBoss].lifeMax / 6)
					{
						npc.ai[2] += Counter;
					}
					bool flag43 = false;
					if (!Collision.CanHit(Main.npc[NPC.golemBoss].Center, 1, 1, Main.player[npc.target].Center, 1, 1))
					{
						flag43 = true;
					}
					if (flag43)
					{
						npc.ai[2] += Counter * 10f;
					}
					if (npc.ai[2] > (float)(100 + Main.rand.Next(4800)))
					{
						npc.ai[2] = 0f;
						for (int num754 = 0; num754 < 2; num754++)
						{
							Vector2 vector86 = new Vector2(npc.Center.X, npc.Center.Y - 50f);
							if (DRGNModWorld.MentalMode)
							{
								vector86.Y += 30f;
							}
							switch (num754)
							{
								case 0:
									vector86.X -= 14f;
									break;
								case 1:
									vector86.X += 14f;
									break;
							}
							float num755 = 11f;
							int num756 = 24;
							int num757 = 259;
							if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax * 0.5)
							{
								num756++;
								num755 += 0.25f;
							}
							if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax * 0.4)
							{
								num756++;
								num755 += 0.25f;
							}
							if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax * 0.3)
							{
								num756++;
								num755 += 0.25f;
							}
							if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax * 0.2)
							{
								num756++;
								num755 += 0.25f;
							}
							if ((double)Main.npc[NPC.golemBoss].life < (double)Main.npc[NPC.golemBoss].lifeMax * 0.1)
							{
								num756++;
								num755 += 0.25f;
							}
							float num758 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f;
							float num759 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f;
							if (flag43)
							{
								num756 = (int)((double)num756 * 1.5);
								num755 *= 2.5f;
								num758 += Main.player[npc.target].velocity.X * Main.rand.NextFloat() * 50f;
								num759 += Main.player[npc.target].velocity.Y * Main.rand.NextFloat() * 50f;
							}
							num758 -= vector86.X;
							num759 -= vector86.Y;
							float num760 = (float)Math.Sqrt(num758 * num758 + num759 * num759);
							num760 = num755 / num760;
							num758 *= num760;
							num759 *= num760;
							vector86.X += num758 * 3f;
							vector86.Y += num759 * 3f;
							if (Main.netMode != 1)
							{
								int num761 = Projectile.NewProjectile(vector86.X, vector86.Y, num758, num759, num757, num756, 0f, Main.myPlayer);
								Main.projectile[num761].timeLeft = 300;
							}
						}
					}
					npc.position += Vector2.Zero;
					int num762 = Main.rand.Next(2) * 2 - 1;
					Vector2 position4 = npc.Bottom + new Vector2(num762 * 22, -22f);
					Dust dust5 = Dust.NewDustPerfect(position4, 228, ((float)Math.PI / 2f + -(float)Math.PI / 2f * (float)num762 + Main.rand.NextFloatDirection() * ((float)Math.PI / 4f)).ToRotationVector2() * (2f + Main.rand.NextFloat()));
					Dust dust = dust5;
					dust.velocity += npc.velocity;
					dust5.noGravity = true;
					dust5 = Dust.NewDustPerfect(npc.Bottom + new Vector2(Main.rand.NextFloatDirection() * 6f, Main.rand.NextFloat() * -4f - 8f), 228, Vector2.UnitY * (2f + Main.rand.NextFloat()));
					dust5.fadeIn = 0f;
					dust5.scale = 0.7f + Main.rand.NextFloat() * 0.5f;
					dust5.noGravity = true;
					dust = dust5;
					dust.velocity += npc.velocity;
					npc.position -= Vector2.Zero;
				}
                else { return true; }
				return false;
			}
			return true;
        }
		private  bool SolidTiles(Vector2 position, int width, int height)
		{
			return SolidTiles((int)(position.X / 16f), (int)((position.X + (float)width) / 16f), (int)(position.Y / 16f), (int)((position.Y + (float)height) / 16f));
		}

		private  bool SolidTiles(int startX, int endX, int startY, int endY)
		{
			if (startX < 0)
			{
				return true;
			}
			if (endX >= Main.maxTilesX)
			{
				return true;
			}
			if (startY < 0)
			{
				return true;
			}
			if (endY >= Main.maxTilesY)
			{
				return true;
			}
			for (int vector = startX; vector < endX + 1; vector++)
			{
				for (int i = startY; i < endY + 1; i++)
				{
					if (Main.tile[vector, i] == null)
					{
						return false;
					}
					if (Main.tile[vector, i].active() && !Main.tile[vector, i].inActive() && Main.tileSolid[Main.tile[vector, i].type] && !Main.tileSolidTop[Main.tile[vector, i].type])
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}