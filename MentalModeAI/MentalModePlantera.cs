using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DRGN.MentalModeAI
{
    public class MentalModePlantera : GlobalNPC


    {

       
        private static int TPCD = 400;
		private static bool justTP = false;
        public override bool PreAI(NPC npc)
        {
            if (DRGNModWorld.MentalMode)
            {
				if (npc.aiStyle == 51)
				{
                    if (justTP) { justTP = false; }
					npc.TargetClosest();
					if (TPCD > 0) { TPCD -= 1; }
					if(TPCD == 0) 
					{ 
						for (int i = 0; i < 50; i++) 
						{ Dust.NewDust(npc.position, npc.width, npc.height, 243); } 
						
						Vector2 Norm = Vector2.Normalize(Main.player[npc.target].Center - npc.Center); 
						npc.Center +=  (Norm* 200)  + (Norm * npc.Distance(Main.player[npc.target].Center));
						TPCD = (npc.life < npc.lifeMax * 0.75) ? 200 : 400;
						justTP = true; 
					}
					bool PlayerDead = false;
					bool DeadPlayer = false;
					
					if (Main.player[npc.target].dead)
					{
						DeadPlayer = true;
						PlayerDead = true;
					}
					if (Main.netMode != 1)
					{
						int num770 = 6000;
						if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num770)
						{
							npc.active = false;
							npc.life = 0;
							if (Main.netMode == 2)
							{
								NetMessage.SendData(23, -1, -1, null, npc.whoAmI);
							}
						}
					}
					NPC.plantBoss = npc.whoAmI;
					if (npc.localAI[0] == 0f && Main.netMode != 1)
					{
						npc.localAI[0] = 1f;
						int npcid = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PlanterasHook, npc.whoAmI);
						npcid = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PlanterasHook, npc.whoAmI);
						npcid = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PlanterasHook, npc.whoAmI);
					}
					int[] array2 = new int[3];
					float X = 0f;
					float Y = 0f;
					int LoopCycles = 0;
					for (int i = 0; i < 200; i++)
					{
						if (Main.npc[i].active && Main.npc[i].aiStyle == 52)
						{
							X += Main.npc[i].Center.X;
							Y += Main.npc[i].Center.Y;
							array2[LoopCycles] = i;
							LoopCycles++;
							if (LoopCycles > 2)
							{
								break;
							}
						}
					}
					X /= (float)LoopCycles;
					Y /= (float)LoopCycles;
					float Speed = 2.5f;
					float Acceleration = 0.025f;
					if (npc.life < npc.lifeMax / 2)
					{
						Speed = 6f;
						Acceleration = 0.1f;
					}
					if (npc.life < npc.lifeMax / 4)
					{
						Speed = 8f;
					}
					if (!Main.player[npc.target].ZoneJungle || (double)Main.player[npc.target].position.Y < Main.worldSurface * 16.0 || Main.player[npc.target].position.Y > (float)((Main.maxTilesY - 200) * 16))
					{
						PlayerDead = true;
						Speed += 8f;
						Acceleration = 0.15f;
					}
					if (Main.expertMode)
					{
						Speed += 1f;
						Speed *= 1.1f;
						Acceleration += 0.01f;
						Acceleration *= 1.1f;
					}
					if (DRGNModWorld.MentalMode)
					{
						Speed *= 1.5f;
						Acceleration *= 2f;
					}
					Vector2 Possiblecenter = new Vector2(X, Y);
					float Xdiff = Main.player[npc.target].Center.X - Possiblecenter.X;
					float Ydiff = Main.player[npc.target].Center.Y - Possiblecenter.Y;
					if (DeadPlayer)
					{
						Ydiff *= -1f;
						Xdiff *= -1f;
						Speed += 8f;
					}
					float Mag = (float)Math.Sqrt(Xdiff * Xdiff + Ydiff * Ydiff);
					int Speediness = 500;
					if (PlayerDead)
					{
						Speediness += 350;
					}
					if (Main.expertMode)
					{
						Speediness += 150;
					}
					if (Mag >= (float)Speediness)
					{
						Mag = (float)Speediness / Mag;
						Xdiff *= Mag;
						Ydiff *= Mag;
					}
					X += Xdiff;
					Y += Ydiff;
					Possiblecenter = new Vector2(npc.Center.X, npc.Center.Y);
					Xdiff = X - Possiblecenter.X;
					Ydiff = Y - Possiblecenter.Y;
					Mag = (float)Math.Sqrt(Xdiff * Xdiff + Ydiff * Ydiff);
					if (Mag < Speed)
					{
						Xdiff = npc.velocity.X;
						Ydiff = npc.velocity.Y;
					}
					else
					{
						Mag = Speed / Mag;
						Xdiff *= Mag;
						Ydiff *= Mag;
					}
					if (npc.velocity.X < Xdiff)
					{
						npc.velocity.X += Acceleration;
						if (npc.velocity.X < 0f && Xdiff > 0f)
						{
							npc.velocity.X += Acceleration * 2f;
						}
					}
					else if (npc.velocity.X > Xdiff)
					{
						npc.velocity.X -= Acceleration;
						if (npc.velocity.X > 0f && Xdiff < 0f)
						{
							npc.velocity.X -= Acceleration * 2f;
						}
					}
					if (npc.velocity.Y < Ydiff)
					{
						npc.velocity.Y += Acceleration;
						if (npc.velocity.Y < 0f && Ydiff > 0f)
						{
							npc.velocity.Y += Acceleration * 2f;
						}
					}
					else if (npc.velocity.Y > Ydiff)
					{
						npc.velocity.Y -= Acceleration;
						if (npc.velocity.Y > 0f && Ydiff < 0f)
						{
							npc.velocity.Y -= Acceleration * 2f;
						}
					}
					Vector2 NPCCenter = new Vector2(npc.Center.X, npc.Center.Y);
					float XDiff = Main.player[npc.target].Center.X - NPCCenter.X;
					float YDiff = Main.player[npc.target].Center.Y - NPCCenter.Y;
					npc.rotation = (float)Math.Atan2(YDiff, XDiff) + 1.57f;
					if (npc.life > npc.lifeMax * 0.75)
					{
						npc.defense = 36;
						int damage = 50;
						if (PlayerDead)
						{
							npc.defense *= 2;
							damage *= 2;
						}
						npc.damage = damage;
						if (Main.netMode == 1)
						{
							return false;
						}
						npc.localAI[1] += 1f;
						if ((double)npc.life < (double)npc.lifeMax * 0.9)
						{
							npc.localAI[1] += 1f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.8)
						{
							npc.localAI[1] += 1f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.7)
						{
							npc.localAI[1] += 1f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.6)
						{
							npc.localAI[1] += 1f;
						}
						if (PlayerDead)
						{
							npc.localAI[1] += 3f;
						}
						if (Main.expertMode)
						{
							npc.localAI[1] += 1f;
						}
						if (Main.expertMode && npc.justHit && Main.rand.Next(2) == 0)
						{
							npc.localAI[3] = 1f;
						}
						if (DRGNModWorld.MentalMode)
						{
							npc.localAI[1] += 1f;
						}
						if (!(npc.localAI[1] > 80f))
						{
							return false;
						}
						npc.localAI[1] = 0f;

						if (npc.localAI[3] > 0f)
						{

							npc.localAI[3] = 0f;
						}

							Vector2 Center = new Vector2(npc.Center.X, npc.Center.Y);
							float SpeedMax = 15f;
							if (Main.expertMode)
							{
								SpeedMax = 17f;
							}
							float CalcXdiff = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - Center.X;
							float CalcYdiff = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - Center.Y;
							float Magnitude = (float)Math.Sqrt(CalcXdiff * CalcXdiff + CalcYdiff * CalcYdiff);
							Magnitude = SpeedMax / Magnitude;
							CalcXdiff *= Magnitude;
							CalcYdiff *= Magnitude;
							int ProjDamage = 22;
							int[] petals = new int[6] { ProjectileID.FlowerPetal, ProjectileID.FlowerPowPetal, ProjectileID.CrystalLeafShot,275,276,277 };
							
							int maxValue2 = 4;
							int maxValue3 = 8;
							if (Main.expertMode)
							{
								maxValue2 = 2;
								maxValue3 = 6;
							}
							if ((double)npc.life < (double)npc.lifeMax * 0.8 && Main.rand.Next(maxValue2) == 0)
							{
								ProjDamage = 27;
								npc.localAI[1] = -30f;
							
							}
							else if ((double)npc.life < (double)npc.lifeMax * 0.8 && Main.rand.Next(maxValue3) == 0)
							{
								ProjDamage = 31;
								npc.localAI[1] = -120f;
								
							}
							if (PlayerDead)
							{
								ProjDamage *= 2;
							}

							Center.X += CalcXdiff * 3f;
							Center.Y += CalcYdiff * 3f;


							for (int i = 0; i < 3; i++)
							{
								int Projid = Projectile.NewProjectile(Center.X, Center.Y, CalcXdiff, CalcYdiff, Main.rand.Next(petals), ProjDamage, 0f, Main.myPlayer);

								Main.projectile[Projid].hostile = true;
								Main.projectile[Projid].friendly = false;
							}
						
						return false;
					}
					npc.defense = 10;
					int Dmg = 70;
					if (PlayerDead)
					{
						npc.defense *= 4;
						Dmg *= 2;
					}
					npc.damage = Dmg;
					if (Main.netMode != 1)
					{
						if (npc.localAI[0] == 1f)
						{
							npc.localAI[0] = 2f;
							int numNPCs = 8;
							if (DRGNModWorld.MentalMode)
							{
								numNPCs += 6;
							}
							for (int i = 0; i < numNPCs; i++)
							{
								int npcid = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, 264, npc.whoAmI);
							}
							if (Main.expertMode)
							{
								for (int i = 0; i < 200; i++)
								{
									if (Main.npc[i].active && Main.npc[i].aiStyle == 52)
									{
										for (int j = 0; j < numNPCs / 2 - 1; j++)
										{
											int npcid = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, 264, npc.whoAmI);
											Main.npc[npcid].ai[3] = i + 1;
										}
									}
								}
							}
						}
						else if (Main.expertMode && Main.rand.Next(60) == 0)
						{
							int numberofTentacles = 0;
							for (int i = 0; i < 200; i++)
							{
								if (Main.npc[i].active && Main.npc[i].type == 264 && Main.npc[i].ai[3] == 0f)
								{
									numberofTentacles++;
								}
							}
							if (numberofTentacles < 20 && Main.rand.Next((numberofTentacles + 1) * 10) <= 3)
							{
								int npcid = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, 264, npc.whoAmI);
							}
						}
					}
					if (npc.localAI[2] == 0f)
					{
						Gore.NewGore(new Vector2(npc.position.X + (float)Main.rand.Next(npc.width), npc.position.Y + (float)Main.rand.Next(npc.height)), npc.velocity, 378, npc.scale);
						Gore.NewGore(new Vector2(npc.position.X + (float)Main.rand.Next(npc.width), npc.position.Y + (float)Main.rand.Next(npc.height)), npc.velocity, 379, npc.scale);
						Gore.NewGore(new Vector2(npc.position.X + (float)Main.rand.Next(npc.width), npc.position.Y + (float)Main.rand.Next(npc.height)), npc.velocity, 380, npc.scale);
						npc.localAI[2] = 1f;
					}
					npc.localAI[1] += 1f;
					if ((double)npc.life < (double)npc.lifeMax * 0.4)
					{
						npc.localAI[1] += 1f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.3)
					{
						npc.localAI[1] += 1f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.2)
					{
						npc.localAI[1] += 1f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.1)
					{
						npc.localAI[1] += 1f;
					}
					if (npc.localAI[1] >= 350f)
					{
						float speed = 8f;
						Vector2 npcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float xdiff = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - npcCenter.X + (float)Main.rand.Next(-10, 11);
						float absXmult = Math.Abs(xdiff * 0.2f);
						float ydiff = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - npcCenter.Y + (float)Main.rand.Next(-10, 11);
						if (ydiff > 0f)
						{
							absXmult = 0f;
						}
						ydiff -= absXmult;
						float mag = (float)Math.Sqrt(xdiff * xdiff + ydiff * ydiff);
						mag = speed / mag;
						xdiff *= mag;
						ydiff *= mag;
						int npcid = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, 265);
						Main.npc[npcid].velocity.X = xdiff;
						Main.npc[npcid].velocity.Y = ydiff;
						Main.npc[npcid].netUpdate = true;
						npc.localAI[1] = 0f;
					}
				}
				else if (npc.aiStyle == 52)
				{
					if (justTP) { npc.Center = Main.npc[NPC.plantBoss].Center; }
					bool enraged = false;
					bool playerDead = false;
					if (NPC.plantBoss < 0)
					{
						npc.StrikeNPCNoInteraction(9999, 0f, 0);
						npc.netUpdate = true;
						return false;
					}
					if (Main.player[Main.npc[NPC.plantBoss].target].dead)
					{
						playerDead = true;
					}
					if ((NPC.plantBoss != -1 && !Main.player[Main.npc[NPC.plantBoss].target].ZoneJungle) || (double)Main.player[Main.npc[NPC.plantBoss].target].position.Y < Main.worldSurface * 16.0 || Main.player[Main.npc[NPC.plantBoss].target].position.Y > (float)((Main.maxTilesY - 200) * 16) || playerDead)
					{
						npc.localAI[0] -= 4f;
						enraged = true;
					}
					if (Main.netMode == 1)
					{
						if (npc.ai[0] == 0f)
						{
							npc.ai[0] = (int)(npc.Center.X / 16f);
						}
						if (npc.ai[1] == 0f)
						{
							npc.ai[1] = (int)(npc.Center.X / 16f);
						}
					}
					if (Main.netMode != 1)
					{
						if (npc.ai[0] == 0f || npc.ai[1] == 0f)
						{
							npc.localAI[0] = 0f;
						}
						npc.localAI[0] -= 1f;
						if (Main.npc[NPC.plantBoss].life < Main.npc[NPC.plantBoss].lifeMax / 2)
						{
							npc.localAI[0] -= 2f;
						}
						if (Main.npc[NPC.plantBoss].life < Main.npc[NPC.plantBoss].lifeMax / 4)
						{
							npc.localAI[0] -= 2f;
						}
						if (enraged)
						{
							npc.localAI[0] -= 6f;
						}
						if (!playerDead && npc.localAI[0] <= 0f && npc.ai[0] != 0f)
						{
							for (int i = 0; i < 200; i++)
							{
								if (i != npc.whoAmI && Main.npc[i].active && Main.npc[i].type == npc.type && (Main.npc[i].velocity.X != 0f || Main.npc[i].velocity.Y != 0f))
								{
									npc.localAI[0] = Main.rand.Next(60, 300);
								}
							}
						}
						if (npc.localAI[0] <= 0f)
						{
							npc.localAI[0] = Main.rand.Next(300, 600);
							bool Hooked = false;
							int attempts = 0;
							while (!Hooked && attempts <= 1000)
							{
								attempts++;
								int playertileX = (int)(Main.player[Main.npc[NPC.plantBoss].target].Center.X / 16f);
								int playerTileY = (int)(Main.player[Main.npc[NPC.plantBoss].target].Center.Y / 16f);
								if (npc.ai[0] == 0f)
								{
									playertileX = (int)((Main.player[Main.npc[NPC.plantBoss].target].Center.X + Main.npc[NPC.plantBoss].Center.X) / 32f);
									playerTileY = (int)((Main.player[Main.npc[NPC.plantBoss].target].Center.Y + Main.npc[NPC.plantBoss].Center.Y) / 32f);
								}
								if (playerDead)
								{
									playertileX = (int)Main.npc[NPC.plantBoss].position.X / 16;
									playerTileY = (int)(Main.npc[NPC.plantBoss].position.Y + 400f) / 16;
								}
								int Rand = 20;
								Rand += (int)(100f * ((float)attempts / 1000f));
								int NearPlayerTileX = playertileX + Main.rand.Next(-Rand, Rand + 1);
								int NearPlayerTileY = playerTileY + Main.rand.Next(-Rand, Rand + 1);
								if (Main.npc[NPC.plantBoss].life < Main.npc[NPC.plantBoss].lifeMax / 2 && Main.rand.Next(6) == 0)
								{
									npc.TargetClosest();
									int PlayerTileX = (int)(Main.player[npc.target].Center.X / 16f);
									int PlayerTileY = (int)(Main.player[npc.target].Center.Y / 16f);
									if (Main.tile[PlayerTileX, PlayerTileY].wall > 0)
									{
										NearPlayerTileX = PlayerTileX;
										NearPlayerTileY = PlayerTileY;
									}
								}
								try
								{
									if (WorldGen.InWorld(NearPlayerTileX, NearPlayerTileY) && (WorldGen.SolidTile(NearPlayerTileX, NearPlayerTileY) || (Main.tile[NearPlayerTileX, NearPlayerTileY].wall > 0 && (attempts > 500 || Main.npc[NPC.plantBoss].life < Main.npc[NPC.plantBoss].lifeMax / 2))))
									{
										Hooked = true;
										npc.ai[0] = NearPlayerTileX;
										npc.ai[1] = NearPlayerTileY;
										npc.netUpdate = true;
									}
								}
								catch
								{
								}
							}
						}
					}
					if (npc.ai[0] > 0f && npc.ai[1] > 0f)
					{
						float MaxDist = 6f;
						if (Main.npc[NPC.plantBoss].life < Main.npc[NPC.plantBoss].lifeMax / 2)
						{
							MaxDist = 8f;
						}
						if (Main.npc[NPC.plantBoss].life < Main.npc[NPC.plantBoss].lifeMax / 4)
						{
							MaxDist = 10f;
						}
						if (Main.expertMode)
						{
							MaxDist += 2f;
						}
						if (Main.expertMode && Main.npc[NPC.plantBoss].life < Main.npc[NPC.plantBoss].lifeMax / 2)
						{
							MaxDist += 4f;
						}
						if (enraged)
						{
							MaxDist *= 2f;
						}
						if (playerDead)
						{
							MaxDist *= 2f;
						}
						Vector2 NpcCenter = new Vector2(npc.Center.X, npc.Center.Y);
						float Xdiff = npc.ai[0] * 16f - 8f - NpcCenter.X;
						float Ydiff = npc.ai[1] * 16f - 8f - NpcCenter.Y;
						float Mag = (float)Math.Sqrt(Xdiff * Xdiff + Ydiff * Ydiff);
						if (Mag < 12f + MaxDist)
						{
							npc.velocity.X = Xdiff;
							npc.velocity.Y = Ydiff;
						}
						else
						{
							Mag = MaxDist / Mag;
							npc.velocity.X = Xdiff * Mag;
							npc.velocity.Y = Ydiff * Mag;
						}
						Vector2 NPCCenter = new Vector2(npc.Center.X, npc.Center.Y);
						float XDiff = Main.npc[NPC.plantBoss].Center.X - NPCCenter.X;
						float YDiff = Main.npc[NPC.plantBoss].Center.Y - NPCCenter.Y;
						npc.rotation = (float)Math.Atan2(YDiff, XDiff) - 1.57f;
					}
				}
				else if (npc.aiStyle == 53)
				{
					if (justTP) { npc.Center = Main.npc[NPC.plantBoss].Center; }
					if (NPC.plantBoss < 0)
					{
						npc.StrikeNPCNoInteraction(9999, 0f, 0);
						npc.netUpdate = true;
						return false;
					}
					int planteraID = NPC.plantBoss;
					if (npc.ai[3] > 0f)
					{
						planteraID = (int)npc.ai[3] - 1;
					}
					if (Main.netMode != 1)
					{
						npc.localAI[0] -= 1f;
						if (npc.localAI[0] <= 0f)
						{
							npc.localAI[0] = Main.rand.Next(120, 480);
							npc.ai[0] = Main.rand.Next(-200, 201);
							npc.ai[1] = Main.rand.Next(-200, 201);
							npc.netUpdate = true;
						}
					}
					npc.TargetClosest();
					float Acceleration = 1f;
					float Speed = 200f;
					if ((double)Main.npc[NPC.plantBoss].life < (double)Main.npc[NPC.plantBoss].lifeMax * 0.25)
					{
						Speed += 100f;
					}
					if ((double)Main.npc[NPC.plantBoss].life < (double)Main.npc[NPC.plantBoss].lifeMax * 0.1)
					{
						Speed += 100f;
					}
					if (Main.expertMode)
					{
						float HpPercent = 1f - (float)npc.life / (float)npc.lifeMax;
						Speed += HpPercent * 300f;
						Acceleration += 0.3f;
					}
					
					if (!Main.npc[planteraID].active || NPC.plantBoss < 0)
					{
						npc.active = false;
						return false;
					}
					float PlanteraX = Main.npc[planteraID].position.X + (float)(Main.npc[planteraID].width / 2);
					float PlanteraY = Main.npc[planteraID].position.Y + (float)(Main.npc[planteraID].height / 2);
					Vector2 PlanteraCenter = new Vector2(PlanteraX, PlanteraY);
					float Xoffset = PlanteraX + npc.ai[0];
					float Yoffset = PlanteraY + npc.ai[1];
					float Xdiff = Xoffset - PlanteraCenter.X;
					float Ydiff = Yoffset - PlanteraCenter.Y;
					float Mag = (float)Math.Sqrt(Xdiff * Xdiff + Ydiff * Ydiff);
					Mag = Speed / Mag;
					Xdiff *= Mag;
					Ydiff *= Mag;
					if (npc.position.X < PlanteraX + Xdiff)
					{
						npc.velocity.X += Acceleration;
						if (npc.velocity.X < 0f && Xdiff > 0f)
						{
							npc.velocity.X *= 0.9f;
						}
					}
					else if (npc.position.X > PlanteraX + Xdiff)
					{
						npc.velocity.X -= Acceleration;
						if (npc.velocity.X > 0f && Xdiff < 0f)
						{
							npc.velocity.X *= 0.9f;
						}
					}
					if (npc.position.Y < PlanteraY + Ydiff)
					{
						npc.velocity.Y += Acceleration;
						if (npc.velocity.Y < 0f && Ydiff > 0f)
						{
							npc.velocity.Y *= 0.9f;
						}
					}
					else if (npc.position.Y > PlanteraY + Ydiff)
					{
						npc.velocity.Y -= Acceleration;
						if (npc.velocity.Y > 0f && Ydiff < 0f)
						{
							npc.velocity.Y *= 0.9f;
						}
					}
					if (npc.velocity.X > 8f)
					{
						npc.velocity.X = 8f;
					}
					if (npc.velocity.X < -8f)
					{
						npc.velocity.X = -8f;
					}
					if (npc.velocity.Y > 8f)
					{
						npc.velocity.Y = 8f;
					}
					if (npc.velocity.Y < -8f)
					{
						npc.velocity.Y = -8f;
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
				}
				else { return true; }
					return false;
				
			
			}
			return true;
		}
    }
}
                