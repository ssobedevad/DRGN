using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.MentalModeAI
{
    public class MentalModeWOF : GlobalNPC


    {

        public static int shootCD;

        public override bool PreAI(NPC npc)
        {
			if (DRGNModWorld.MentalMode)
			{
				if (npc.aiStyle == 27)
				{
					if (npc.position.X < 160f || npc.position.X > (float)((Main.maxTilesX - 10) * 16))
					{
						npc.active = false;
					}
					if (npc.localAI[0] == 0f)
					{
						npc.localAI[0] = 1f;
						Main.wofB = -1;
						Main.wofT = -1;
					}
					npc.ai[1] += 1f;
					if (npc.ai[2] == 0f)
					{
						if ((double)npc.life < (double)npc.lifeMax * 0.5)
						{
							npc.ai[1] += 1f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.2)
						{
							npc.ai[1] += 1f;
						}
						if (npc.ai[1] > 2700f)
						{
							npc.ai[2] = 1f;
						}
					}
					if (npc.ai[2] > 0f && npc.ai[1] > 60f)
					{
						int num345 = 3;
						if ((double)npc.life < (double)npc.lifeMax * 0.3)
						{
							num345++;
						}
						npc.ai[2] += 1f;
						npc.ai[1] = 0f;
						if (npc.ai[2] > (float)num345)
						{
							npc.ai[2] = 0f;
						}
						if (Main.netMode != 1)
						{
							int num346 = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)(npc.height / 2) + 20f), 117, 1);
							Main.npc[num346].velocity.X = npc.direction * 8;
						}
					}
					npc.localAI[3] += 1f;
					if (npc.localAI[3] >= (float)(600 + Main.rand.Next(1000)))
					{
						npc.localAI[3] = -Main.rand.Next(200);
						Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 10);
					}
					Main.wof = npc.whoAmI;
					int num347 = (int)(npc.position.X / 16f);
					int num348 = (int)((npc.position.X + (float)npc.width) / 16f);
					int num349 = (int)((npc.position.Y + (float)(npc.height / 2)) / 16f);
					int num350 = 0;
					int num351 = num349 + 7;
					while (num350 < 15 && num351 > Main.maxTilesY - 10)
					{
						num351++;
						for (int num352 = num347; num352 <= num348; num352++)
						{
							try
							{
								if (WorldGen.SolidTile(num352, num351) || Main.tile[num352, num351].liquid > 0)
								{
									num350++;
								}
							}
							catch
							{
								num350 += 15;
							}
						}
					}
					num351 += 4;
					if (Main.wofB == -1)
					{
						Main.wofB = num351 * 16;
					}
					else if (Main.wofB > num351 * 16)
					{
						Main.wofB--;
						if (Main.wofB < num351 * 16)
						{
							Main.wofB = num351 * 16;
						}
					}
					else if (Main.wofB < num351 * 16)
					{
						Main.wofB++;
						if (Main.wofB > num351 * 16)
						{
							Main.wofB = num351 * 16;
						}
					}
					num350 = 0;
					num351 = num349 - 7;
					while (num350 < 15 && num351 < Main.maxTilesY - 10)
					{
						num351--;
						for (int num353 = num347; num353 <= num348; num353++)
						{
							try
							{
								if (WorldGen.SolidTile(num353, num351) || Main.tile[num353, num351].liquid > 0)
								{
									num350++;
								}
							}
							catch
							{
								num350 += 15;
							}
						}
					}
					num351 -= 4;
					if (Main.wofT == -1)
					{
						Main.wofT = num351 * 16;
					}
					else if (Main.wofT > num351 * 16)
					{
						Main.wofT--;
						if (Main.wofT < num351 * 16)
						{
							Main.wofT = num351 * 16;
						}
					}
					else if (Main.wofT < num351 * 16)
					{
						Main.wofT++;
						if (Main.wofT > num351 * 16)
						{
							Main.wofT = num351 * 16;
						}
					}
					float WofMid = (Main.wofB + Main.wofT) / 2 - npc.height / 2;
					npc.target = -1;
					npc.TargetClosest();
					if (npc.target == -1) { Main.wof = -1; npc.active = false; }
					float targetY = Main.player[npc.target].Center.Y;
					npc.velocity.Y = (targetY > npc.Center.Y )? 2 : -2;
					float Speediness = 0.01f;
					if ((double)npc.life < (double)npc.lifeMax)
					{
						Speediness += 1f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.7)
					{
						Speediness += 1f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.6)
					{
						Speediness += 1f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.5)
					{
						Speediness += 1f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.4)
					{
						Speediness += 1f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.3)
					{
						Speediness += 1f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.1)
					{
						Speediness += 1f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.05 )
					{
						Speediness += 1f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.01 )
					{
						Speediness += 1f;
					}
					Speediness *= 1.1f;
					if (npc.velocity.X == 0f)
					{
						npc.TargetClosest();
						if (Main.player[npc.target].dead)
						{
							float num357 = float.PositiveInfinity;
							int direction2 = 0;
							for (int num358 = 0; num358 < 255; num358++)
							{
								Player player = Main.player[npc.target];
								if (player.active)
								{
									float num359 = npc.Distance(player.Center);
									if (num357 > num359)
									{
										num357 = num359;
										direction2 = ((npc.Center.X < player.Center.X) ? 1 : (-1));
									}
								}
							}
							npc.direction = direction2;
						}
						npc.velocity.X = npc.direction;
					}
					if (npc.velocity.X < 0f)
					{
						npc.velocity.X = 0f - Speediness;
						npc.direction = -1;
					}
					else
					{
						npc.velocity.X = Speediness;
						npc.direction = 1;
					}
					npc.spriteDirection = npc.direction;
					Vector2 vector34 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float num360 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector34.X;
					float num361 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector34.Y;
					float num362 = (float)Math.Sqrt(num360 * num360 + num361 * num361);
					float num363 = num362;
					num360 *= num362;
					num361 *= num362;
					if (npc.direction > 0)
					{
						if (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) > npc.position.X + (float)(npc.width / 2))
						{
							npc.rotation = (float)Math.Atan2(0f - num361, 0f - num360) + 3.14f;
						}
						else
						{
							npc.rotation = 0f;
						}
					}
					else if (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) < npc.position.X + (float)(npc.width / 2))
					{
						npc.rotation = (float)Math.Atan2(num361, num360) + 3.14f;
					}
					else
					{
						npc.rotation = 0f;
					}
					if (Main.expertMode && Main.netMode != 1)
					{
						int num364 = (int)(1f + (float)npc.life / (float)npc.lifeMax * 10f);
						num364 *= num364;
						if (num364 < 400)
						{
							num364 = (num364 * 19 + 400) / 20;
						}
						if (num364 < 60)
						{
							num364 = (num364 * 3 + 60) / 4;
						}
						if (num364 < 20)
						{
							num364 = (num364 + 20) / 2;
						}
						num364 = (int)((double)num364 * 0.7);
						if (Main.rand.Next(num364) == 0)
						{
							int num365 = 0;
							float[] array = new float[10];
							for (int num366 = 0; num366 < 200; num366++)
							{
								if (num365 < 10 && Main.npc[num366].active && Main.npc[num366].type == 115)
								{
									array[num365] = Main.npc[num366].ai[0];
									num365++;
								}
							}
							int maxValue = 1 + num365 * 2;
							if (num365 < 10 && Main.rand.Next(maxValue) <= 1)
							{
								int num367 = -1;
								for (int num368 = 0; num368 < 1000; num368++)
								{
									int num369 = Main.rand.Next(10);
									float num370 = (float)num369 * 0.1f - 0.05f;
									bool flag27 = true;
									for (int num371 = 0; num371 < num365; num371++)
									{
										if (num370 == array[num371])
										{
											flag27 = false;
											break;
										}
									}
									if (flag27)
									{
										num367 = num369;
										break;
									}
								}
								if (num367 >= 0)
								{
									int num372 = NPC.NewNPC((int)npc.position.X, (int)WofMid, 115, npc.whoAmI);
									Main.npc[num372].ai[0] = (float)num367 * 0.1f - 0.05f;
								}
							}
						}
					}
					if (npc.localAI[0] == 1f && Main.netMode != 1)
					{
						npc.localAI[0] = 2f;
						WofMid = (Main.wofB + Main.wofT) / 2;
						WofMid = (WofMid + (float)Main.wofT) / 2f;
						int ID = NPC.NewNPC((int)npc.position.X, (int)WofMid, 114, npc.whoAmI);
						Main.npc[ID].ai[0] = 1f;
						WofMid = (Main.wofB + Main.wofT) / 2;
						WofMid = (WofMid + (float)Main.wofB) / 2f;
						ID = NPC.NewNPC((int)npc.position.X, (int)WofMid, 114, npc.whoAmI);
						Main.npc[ID].ai[0] = -1f;
						WofMid = (Main.wofB + Main.wofT) / 2;
						WofMid = (WofMid + (float)Main.wofT) / 2f;
						ID = NPC.NewNPC((int)npc.position.X, (int)WofMid, 114, npc.whoAmI);
						Main.npc[ID].ai[0] = 2f;
						WofMid = (Main.wofB + Main.wofT) / 2;
						WofMid = (WofMid + (float)Main.wofB) / 2f;
						ID = NPC.NewNPC((int)npc.position.X, (int)WofMid, 114, npc.whoAmI);
						Main.npc[ID].ai[0] = -2f;
						WofMid = (Main.wofB + Main.wofT) / 2;
						WofMid = (WofMid + (float)Main.wofB) / 2f;
						for (int num374 = 0; num374 < 22; num374++)
						{
							ID = NPC.NewNPC((int)npc.position.X, (int)WofMid, 115, npc.whoAmI);
							Main.npc[ID].ai[0] = (float)num374 * 0.1f - 0.05f;
						}
					}
				}
				if (npc.aiStyle == 28)
				{
					if (Main.wof < 0)
					{
						npc.active = false;
						return false;
					}
					npc.realLife = Main.wof;
					if (Main.npc[Main.wof].life > 0)
					{
						npc.life = Main.npc[Main.wof].life;
					}
					npc.TargetClosest();
					npc.position.X = Main.npc[Main.wof].position.X;
					npc.direction = Main.npc[Main.wof].direction;
					npc.spriteDirection = npc.direction;
					float WofMiddle = (Main.wofB + Main.wofT) / 2;
					if (npc.ai[0] == 2 || npc.ai[0] == -2) { WofMiddle = ((!(npc.ai[0] > 0f)) ? (Main.npc[Main.wof].Center.Y + 300) : (Main.npc[Main.wof].Center.Y - 300)); }
					else
					{
						WofMiddle = ((!(npc.ai[0] > 0f)) ? (Main.npc[Main.wof].Center.Y + 150) : (Main.npc[Main.wof].Center.Y - 150));
					}
					
					WofMiddle -= (float)(npc.height / 2);
					if (npc.position.Y > WofMiddle + 2f)
					{
						npc.velocity.Y = -2f;
					}
					else if (npc.position.Y < WofMiddle - 2f)
					{
						npc.velocity.Y = 2f;
					}
					else
					{
						npc.velocity.Y = 0f;
						npc.position.Y = WofMiddle;
					}
					if (npc.velocity.Y > 5f)
					{
						npc.velocity.Y = 5f;
					}
					if (npc.velocity.Y < -5f)
					{
						npc.velocity.Y = -5f;
					}
					Vector2 vector35 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float num376 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector35.X;
					float num377 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector35.Y;
					float num378 = (float)Math.Sqrt(num376 * num376 + num377 * num377);
					float num379 = num378;
					num376 *= num378;
					num377 *= num378;
					bool flag28 = true;
					if (npc.direction > 0)
					{
						if (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) > npc.position.X + (float)(npc.width / 2))
						{
							npc.rotation = (float)Math.Atan2(0f - num377, 0f - num376) + 3.14f;
						}
						else
						{
							npc.rotation = 0f;
							flag28 = false;
						}
					}
					else if (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) < npc.position.X + (float)(npc.width / 2))
					{
						npc.rotation = (float)Math.Atan2(num377, num376) + 3.14f;
					}
					else
					{
						npc.rotation = 0f;
						flag28 = false;
					}
					if (Main.netMode == 1)
					{
						return false;
					}
					int num380 = 4;
					npc.localAI[1] += 1f;
					if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.95)
					{
						npc.localAI[1] += 1f;
						num380++;
					}
					if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.7)
					{
						npc.localAI[1] += 1f;
						num380++;
					}
					if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.45)
					{
						npc.localAI[1] += 1f;
						num380 += 2;
					}
					if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.3)
					{
						npc.localAI[1] += 2f;
						num380 += 3;
					}
					if (Main.expertMode)
					{
						npc.localAI[1] += 0.5f;
						num380++;
						if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.3)
						{
							npc.localAI[1] += 2f;
							num380 += 3;
						}
					}
					if (npc.localAI[2] == 0f)
					{
						if (npc.localAI[1] > 600f)
						{
							npc.localAI[2] = 1f;
							npc.localAI[1] = 0f;
						}
					}
					else
					{
						if (!(npc.localAI[1] > 45f))
						{
							return false;
						}
						npc.localAI[1] = 0f;
						npc.localAI[2] += 1f;
						if (npc.localAI[2] >= (float)num380)
						{
							npc.localAI[2] = 0f;
						}
						if (flag28)
						{
							float Speed = 12f;
							int num382 = 11;
							int num383 = 83;
							if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.8)
							{
								num382++;
								Speed += 3f;
							}
							if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.65)
							{
								num382++;
								Speed += 2f;
							}
							if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.4)
							{
								num382 += 2;
								Speed += 2f;
							}
							vector35 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
							num376 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector35.X;
							num377 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector35.Y;
							num378 = (float)Math.Sqrt(num376 * num376 + num377 * num377);
							num378 = Speed / num378;
							num376 *= num378;
							num377 *= num378;
							vector35.X += num376;
							vector35.Y += num377;
							int num384 = Projectile.NewProjectile(vector35.X, vector35.Y, num376, num377, num383, num382, 0f, Main.myPlayer);
							Main.projectile[num384].tileCollide = false;
						}
					}
				}
				else if (npc.aiStyle == 29)
				{
					if (npc.justHit)
					{
						npc.ai[1] = 10f;
					}
					if (Main.wof < 0)
					{
						npc.active = false;
						return false;
					}
					npc.TargetClosest();
					float Acceleration = 0.1f;
					float Speed = 300f;
					npc.damage = npc.defDamage;
					int num387 = 0;
					if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.45)
					{
						num387 = 75;
						npc.defense = 40;
						if (!Main.expertMode)
						{
							Speed = 900f;
						}
						else
						{
							Acceleration += 0.1f;
						}
					}
					else if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.7)
					{
						num387 = 60;
						npc.defense = 30;
						if (!Main.expertMode)
						{
							Speed = 700f;
						}
						else
						{
							Acceleration += 0.066f;
						}
					}
					else if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.95)
					{
						num387 = 45;
						npc.defense = 20;
						if (!Main.expertMode)
						{
							Speed = 500f;
						}
						else
						{
							Acceleration += 0.033f;
						}
					}
					if (num387 > 0)
					{
						npc.damage = npc.damage * num387;
					}
					if (Main.expertMode)
					{
						npc.defense = npc.defDefense;
						if (npc.whoAmI % 4 == 0)
						{
							Speed *= 1.75f;
						}
						if (npc.whoAmI % 4 == 1)
						{
							Speed *= 1.5f;
						}
						if (npc.whoAmI % 4 == 2)
						{
							Speed *= 1.25f;
						}
						if (npc.whoAmI % 3 == 0)
						{
							Speed *= 1.5f;
						}
						if (npc.whoAmI % 3 == 1)
						{
							Speed *= 1.25f;
						}
						Speed *= 0.75f;
					}
					float WofCenterX = Main.npc[Main.wof].position.X + (float)(Main.npc[Main.wof].width / 2);
					float WofY = Main.npc[Main.wof].position.Y;
					float WofDiff = Main.wofB - Main.wofT;
					WofY = (float)Main.wofT + WofDiff * npc.ai[0];
					npc.ai[2] += 1f;
					if (npc.ai[2] > 100f)
					{
						Speed = (int)(Speed * 1.3f);
						if (npc.ai[2] > 200f)
						{
							npc.ai[2] = 0f;
						}
					}
					Vector2 vector36 = new Vector2(WofCenterX, WofY);
					float Xdiff = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - (float)(npc.width / 2) - vector36.X;
					float Ydiff = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - (float)(npc.height / 2) - vector36.Y;
					float mag = (float)Math.Sqrt(Xdiff * Xdiff + Ydiff * Ydiff);
					if (npc.ai[1] == 0f)
					{
						if (mag > Speed)
						{
							mag = Speed / mag;
							Xdiff *= mag;
							Ydiff *= mag;
						}
						if (npc.position.X < WofCenterX + Xdiff)
						{
							npc.velocity.X += Acceleration;
							if (npc.velocity.X < 0f && Xdiff > 0f)
							{
								npc.velocity.X += Acceleration * 2.5f;
							}
						}
						else if (npc.position.X > WofCenterX + Xdiff)
						{
							npc.velocity.X -= Acceleration;
							if (npc.velocity.X > 0f && Xdiff < 0f)
							{
								npc.velocity.X -= Acceleration * 2.5f;
							}
						}
						if (npc.position.Y < WofY + Ydiff)
						{
							npc.velocity.Y += Acceleration;
							if (npc.velocity.Y < 0f && Ydiff > 0f)
							{
								npc.velocity.Y += Acceleration * 2.5f;
							}
						}
						else if (npc.position.Y > WofY + Ydiff)
						{
							npc.velocity.Y -= Acceleration;
							if (npc.velocity.Y > 0f && Ydiff < 0f)
							{
								npc.velocity.Y -= Acceleration * 2.5f;
							}
						}
						float num393 = 4f;
						if (Main.expertMode && Main.wof >= 0)
						{
							float num394 = 1.5f;
							float num395 = Main.npc[Main.wof].life / Main.npc[Main.wof].lifeMax;
							if ((double)num395 < 0.75)
							{
								num394 += 0.7f;
							}
							if ((double)num395 < 0.5)
							{
								num394 += 0.7f;
							}
							if ((double)num395 < 0.25)
							{
								num394 += 0.9f;
							}
							if ((double)num395 < 0.1)
							{
								num394 += 0.9f;
							}
							num394 *= 1.25f;
							num394 += 0.3f;
							num393 += num394 * 0.35f;
							if (npc.Center.X < Main.npc[Main.wof].Center.X && Main.npc[Main.wof].velocity.X > 0f)
							{
								num393 += 6f;
							}
							if (npc.Center.X > Main.npc[Main.wof].Center.X && Main.npc[Main.wof].velocity.X < 0f)
							{
								num393 += 6f;
							}
						}
						if (npc.velocity.X > num393)
						{
							npc.velocity.X = num393;
						}
						if (npc.velocity.X < 0f - num393)
						{
							npc.velocity.X = 0f - num393;
						}
						if (npc.velocity.Y > num393)
						{
							npc.velocity.Y = num393;
						}
						if (npc.velocity.Y < 0f - num393)
						{
							npc.velocity.Y = 0f - num393;
						}
					}
					else if (npc.ai[1] > 0f)
					{
						npc.ai[1] -= 1f;
					}
					else
					{
						npc.ai[1] = 0f;
					}
					if (Xdiff > 0f)
					{
						npc.spriteDirection = 1;
						npc.rotation = (float)Math.Atan2(Ydiff, Xdiff);
					}
					if (Xdiff < 0f)
					{
						npc.spriteDirection = -1;
						npc.rotation = (float)Math.Atan2(Ydiff, Xdiff) + 3.14f;
					}
					Lighting.AddLight((int)(npc.position.X + (float)(npc.width / 2)) / 16, (int)(npc.position.Y + (float)(npc.height / 2)) / 16, 0.3f, 0.2f, 0.1f);
				}
				if (npc.aiStyle >= 27 && npc.aiStyle <= 29)
				{
					return false;
				}
			}
			return true;























		}
    }
}